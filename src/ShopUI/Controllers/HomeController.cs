using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShopUI.Models;
using ShopUI.Controllers.Base;

namespace ShopUI.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(IHttpClientFactory httpClientFactory, ILogger<HomeController> logger) : base(httpClientFactory)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
