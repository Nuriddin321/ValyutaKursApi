using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ApiConnect.Models;
using System.Text.Json;

namespace ApiConnect.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public static List<Root>? listData;

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async ValueTask<IActionResult> Valyuta()
    {
        var httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync("https://nbu.uz/uz/exchange-rates/json/");

        string jsonString = response.Content.ReadAsStringAsync().Result;
        listData = JsonSerializer.Deserialize<List<Root>>(jsonString);
        
        return View(listData);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
