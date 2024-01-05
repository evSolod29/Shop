using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.DTO;
using Shared.DTO.DTO.Users;
using ShopUI.Constants;
using ShopUI.Controllers.Base;
using ShopUI.Models;

namespace ShopUI.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        [HttpGet("/login")]
        public IActionResult Login()
        {
            if (User.Identity?.Name != null)
                return RedirectPermanent("/");

            return View();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(UserLoginModel request)
        {
            if (User.Identity?.Name != null)
                return RedirectPermanent("/");

            var content = JsonContent.Create(request);
            var responseMessage = await HttpClient.PostAsync(
                ApiUrls.Login + $"?username={request.UserName}&password={request.Password}",
                content
            );

            if (!responseMessage.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = await responseMessage.Content.ReadAsStringAsync();
                return View();
            }

            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var authDetails = JsonConvert.DeserializeObject<AuthDetails>(responseContent)
                ?? throw new NullReferenceException();
            await Authenticate(authDetails);
            return RedirectPermanent("/");
        }

        [Authorize]
        [HttpPost("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectPermanent("/");
        }

        private async Task Authenticate(AuthDetails details)
        {
            SetTokenCookie(details.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, details.Username));
            identity.AddClaim(new Claim(ClaimTypes.Name, details.Username));
            identity.AddClaim(new Claim(ClaimTypes.Authentication, details.Token));
            foreach (var role in details.Roles)
                identity.AddClaim(new Claim(ClaimTypes.Role, role));

            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.Now.AddMinutes(30),
                IsPersistent = true,
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
        }
    }
}
