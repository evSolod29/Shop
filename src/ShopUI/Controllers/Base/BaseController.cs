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

        protected void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddMinutes(30),
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.None,
            };

            Response.Cookies.Append(AccessToken, token, cookieOptions);
        }

        protected string? GetTokenFromCookie()
        {
            return User.Claims.First(x => x.Type == ClaimTypes.Authentication).Value;
        }
    }
}
