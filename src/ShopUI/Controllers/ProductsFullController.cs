using System;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Shared.DTO.DTO.Categories;
using Shared.DTO.DTO.Products;
using Shared.Resources;
using ShopUI.Constants;
using ShopUI.Controllers.Base;

namespace ShopUI.Controllers
{
    [Authorize(Roles = Roles.SuperUser)]
    [Route("products/full/")]
    public class ProductsFullController : BaseController
    {

        public ProductsFullController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        [HttpGet]
        public async Task<ActionResult> Index(string? name = null,
                                              string? description = null,
                                              decimal? priceFrom = null,
                                              decimal? priceTo = null,
                                              string? commonNote = null,
                                              long? categoryId = null,
                                              long? additionalNote = null)
        {
            var token = GetTokenFromCookie();

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string query = $"?name={name}&description={description}&priceFrom={priceFrom}&" +
                $"priceTo={priceTo}&commonNote={commonNote}&categoryId={categoryId}&additionalNote={additionalNote}";
            var responce = await HttpClient.GetAsync(ApiUrls.ProductsFull + query);
            if (responce.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectPermanent("/login");
            }
            string result = await responce.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<IEnumerable<ViewProductFull>>(result)!;
            TempData["Categories"] = JsonConvert.DeserializeObject<IEnumerable<ViewCategory>>(
                await HttpClient.GetStringAsync(ApiUrls.Categories)
            );
            TempData["Name"] = name;
            TempData["Description"] = description;
            TempData["PriceFrom"] = priceFrom;
            TempData["PriceTo"] = priceTo;
            TempData["CommonNote"] = commonNote;
            TempData["CategoryId"] = categoryId;
            TempData["AdditionalNote"] = additionalNote;


            return View("Index", products);
        }


        [HttpGet("create")]
        public async Task<ActionResult> Create()
        {
            var token = GetTokenFromCookie();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var categories = JsonConvert.DeserializeObject<IEnumerable<ViewCategory>>(
                await HttpClient.GetStringAsync(ApiUrls.Categories)
            );
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }


        [HttpPost("create")]
        public async Task<ActionResult> Create(CreateProduct model)
        {
            var token = GetTokenFromCookie();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = JsonContent.Create(model);
            var response = await HttpClient.PostAsync(ApiUrls.Products, content);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<IEnumerable<ViewCategory>>(
                    await HttpClient.GetStringAsync(ApiUrls.Categories)
                );
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View();
            }
            return RedirectToAction("Index");
        }


        [HttpGet("{id:int}/edit")]
        public async Task<ActionResult> Edit(int id)
        {
            var token = GetTokenFromCookie();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonResponse = await HttpClient.GetAsync(ApiUrls.ProductsFull + id);
            var product = await jsonResponse.Content.ReadFromJsonAsync<ViewProductFull>();

            var categories = JsonConvert.DeserializeObject<IEnumerable<ViewCategory>>(
                await HttpClient.GetStringAsync(ApiUrls.Categories)
            );
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            CreateProduct createProduct = new()
            {
                Name = product!.Name,
                AdditionalNote = product.AdditionalNote,
                CategoryId = product.Category.Id,
                CommonNote = product.CommonNote,
                Description = product.Description,
                Price = product.Price
            };

            return View(createProduct);
        }


        [HttpPost("{id:int}/Edit")]
        public async Task<ActionResult> Edit(int id, CreateProduct model)
        {
            try
            {
                var token = GetTokenFromCookie();
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = JsonContent.Create(model);
                var response = await HttpClient.PutAsync(ApiUrls.Products + id, content);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = await response.Content.ReadAsStringAsync();
                    var categories = JsonConvert.DeserializeObject<IEnumerable<ViewCategory>>(
                        await HttpClient.GetStringAsync(ApiUrls.Categories)
                    );
                    ViewBag.Categories = new SelectList(categories, "Id", "Name");
                    return View();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(model);
            }
        }


        [HttpGet("{id:int}/Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var token = GetTokenFromCookie();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.DeleteAsync(ApiUrls.Products + id);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync();
                return View();
            }
            return RedirectToAction("Index");
        }
    }

}
