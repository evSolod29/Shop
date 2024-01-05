using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Shared.DTO.DTO.Users;
using Shared.Resources;
using ShopUI.Constants;
using ShopUI.Controllers.Base;
using ShopUI.Models;

namespace ShopUI.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [Route("admin/")]
    public class UsersController : BaseController
    {
        public UsersController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        [HttpGet]
        public async Task<ActionResult> Index(string? name = null)
        {
            var token = GetTokenFromCookie();

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string query = $"?name={name}&email=";
            var response = await HttpClient.GetAsync(ApiUrls.Users + query);

            if (response.StatusCode != HttpStatusCode.OK)
                ViewBag.Error = await response.Content.ReadAsStringAsync()
                    ?? "Ошибка";

            string result = await response.Content.ReadAsStringAsync();

            var users = JsonConvert.DeserializeObject<IEnumerable<ViewUser>>(result)!
                .Select(x =>
                {
                    var user = new UserModel()
                    {
                        Email = x.Email,
                        Id = x.Id,
                        IsLocked = x.IsLocked,
                        Name = x.Name
                    };
                    response = HttpClient.GetAsync($"{ApiUrls.Users}{user.Id}/roles/").Result;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        ViewBag.Error = response.Content.ReadAsStringAsync().Result
                            ?? "Ошибка";
                        return user;
                    }
                    user.Roles = JsonConvert.DeserializeObject<IEnumerable<string>>(
                        response.Content.ReadAsStringAsync().Result)!;
                    user.RolesToStr = string.Join(", ", user.Roles);
                    return user;
                })!;
            if (ViewBag.Error != null)
                return View("Index", new List<UserModel>());
            TempData["Name"] = name;

            return View("Index", users);
        }

        [HttpGet("{id}/block")]
        public async Task<ActionResult> Block(string id)
        {
            var token = GetTokenFromCookie();

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.PutAsync($"{ApiUrls.Users}{id}/block", null);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync()
                    ?? "Ошибка";
                return View("Index", new List<UserModel>());
            }

            return RedirectToAction("Index");
        }

        [HttpGet("{id}/unblock")]
        public async Task<ActionResult> Unblock(string id)
        {
            var token = GetTokenFromCookie();

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.PutAsync($"{ApiUrls.Users}{id}/unblock", null);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync()
                    ?? "Ошибка";
                return View("Index", new List<UserModel>());
            }

            return RedirectToAction("Index");
        }

        [HttpGet("{id}/roles/add")]
        public async Task<ActionResult> AddToRole(string id)
        {
            var token = GetTokenFromCookie();

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.GetAsync($"{ApiUrls.Users}{id}/roles");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync()
                    ?? "Ошибка";
                return View("Index", new List<UserModel>());
            }
            var roles = JsonConvert.DeserializeObject<IEnumerable<string>>(
                    await response.Content.ReadAsStringAsync());
            var availableRoles = new List<KeyValuePair<int, string>>();
            if (!roles!.Any(x => x == Roles.User)) availableRoles.Add(new(0, Roles.User));
            if (!roles!.Any(x => x == Roles.SuperUser)) availableRoles.Add(new(1, Roles.SuperUser));
            if (!roles!.Any(x => x == Roles.Admin)) availableRoles.Add(new(2, Roles.Admin));
            ViewBag.Roles = new SelectList(availableRoles, "Key", "Value");
            return View();
        }

        [HttpPost("{id}/roles/add")]
        public async Task<ActionResult> AddToRole(string id, int role)
        {
            var token = GetTokenFromCookie();

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.PostAsync($"{ApiUrls.Users}{id}/roles/{role}", null);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync()
                    ?? "Ошибка";
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet("{id}/roles/remove")]
        public async Task<ActionResult> Remove(string id)
        {
            var token = GetTokenFromCookie();

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.GetAsync($"{ApiUrls.Users}{id}/roles");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync()
                    ?? "Ошибка";
                return View("Index", new List<UserModel>());
            }
            var roles = JsonConvert.DeserializeObject<IEnumerable<string>>(
                    await response.Content.ReadAsStringAsync());
            var availableRoles = new List<KeyValuePair<int, string>>();
            if (roles!.Any(x => x == Roles.User)) availableRoles.Add(new(0, Roles.User));
            if (roles!.Any(x => x == Roles.SuperUser)) availableRoles.Add(new(1, Roles.SuperUser));
            if (roles!.Any(x => x == Roles.Admin)) availableRoles.Add(new(2, Roles.Admin));
            ViewBag.Roles = new SelectList(availableRoles, "Key", "Value");
            return View();
        }

        [HttpPost("{id}/roles/remove")]
        public async Task<ActionResult> Remove(string id, int role)
        {
            var token = GetTokenFromCookie();

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.DeleteAsync($"{ApiUrls.Users}{id}/roles/{role}");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync()
                    ?? "Ошибка";
                return View();
            }

            return RedirectToAction("Index");
        }

    }

}
