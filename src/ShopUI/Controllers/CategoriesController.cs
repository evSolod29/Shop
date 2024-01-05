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
    [Authorize(Roles = Roles.SuperUser)]
    [Route("categories/")]
    public class CategoriesController : BaseController
    {

        public CategoriesController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        [HttpGet]
        public async Task<ActionResult> Index(string? name = null)
        {
            var token = GetTokenFromCookie();

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string query = $"?name={name}";
            var responce = await HttpClient.GetAsync(ApiUrls.Categories + query);
            if (responce.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectPermanent("/login");
            }
            string result = await responce.Content.ReadAsStringAsync();

            var categories = JsonConvert.DeserializeObject<IEnumerable<ViewCategory>>(result)!;
            TempData["Name"] = name;

            return View("Index", categories);
        }


        [HttpGet("create")]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost("create")]
        public async Task<ActionResult> Create(CreateCategory model)
        {
            var token = GetTokenFromCookie();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = JsonContent.Create(model);
            var response = await HttpClient.PostAsync(ApiUrls.Categories, content);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync();
                return View();
            }
            return RedirectToAction("Index");
        }


        [HttpGet("{id:int}/edit")]
        public async Task<ActionResult> Edit(int id)
        {
            var token = GetTokenFromCookie();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonResponse = await HttpClient.GetAsync(ApiUrls.Categories + id);
            var category = await jsonResponse.Content.ReadFromJsonAsync<ViewCategory>();

            CreateCategory createCategory = new()
            {
                Name = category!.Name,
            };

            return View(createCategory);
        }


        [HttpPost("{id:int}/Edit")]
        public async Task<ActionResult> Edit(int id, CreateProduct model)
        {

            var token = GetTokenFromCookie();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = JsonContent.Create(model);
            var response = await HttpClient.PutAsync(ApiUrls.Categories + id, content);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync();
                return View();
            }
            return RedirectToAction("Index");

        }


        [HttpGet("{id:int}/Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var token = GetTokenFromCookie();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.DeleteAsync(ApiUrls.Categories + id);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync();
                return View();
            }
            return RedirectToAction("Index");
        }
    }

}
