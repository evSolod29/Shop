using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ShopUI.Controllers.Base
{
    public class BaseController : Controller
    {
        protected HttpClient HttpClient { get; set; }
        protected const string AccessToken = "AccessToken";

        public BaseController(IHttpClientFactory httpClientFactory)
        {
            HttpClient = httpClientFactory.CreateClient();
        }

        protected string? GetTokenFromCookie()
        {
            return User.Claims.First(x => x.Type == ClaimTypes.Authentication).Value;
        }
    }
}
