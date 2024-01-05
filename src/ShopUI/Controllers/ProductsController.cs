using System;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.DTO.DTO.Categories;
using Shared.DTO.DTO.Products;
using Shared.Resources;
using ShopUI.Constants;
using ShopUI.Controllers.Base;

namespace ShopUI.Controllers
{
    [Route("products/")]
    [Authorize(Roles = Roles.User)]
    public class ProductsController : BaseController
    {
        public ProductsController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<ActionResult> Index(string? name = null,
                                              string? description = null,
                                              decimal? priceFrom = null,
                                              decimal? priceTo = null,
                                              string? commonNote = null,
                                              long? categoryId = null)
        {
            var token = GetTokenFromCookie();

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string query = $"?name={name}&description={description}&priceFrom={priceFrom}&" +
                $"priceTo={priceTo}&commonNote={commonNote}&categoryId={categoryId}";
            var responce = await HttpClient.GetAsync(ApiUrls.Products + query);
            if (responce.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectPermanent("/login");
            }
            string result = await responce.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<IEnumerable<ViewProduct>>(result)!;
            TempData["Categories"] = JsonConvert.DeserializeObject<IEnumerable<ViewCategory>>(
                await HttpClient.GetStringAsync(ApiUrls.Categories)
            );
            TempData["Name"] = name;
            TempData["Description"] = description;
            TempData["PriceFrom"] = priceFrom;
            TempData["PriceTo"] = priceTo;
            TempData["CommonNote"] = commonNote;
            TempData["CategoryId"] = categoryId;


            return View("Index", products);
        }
    }

}
