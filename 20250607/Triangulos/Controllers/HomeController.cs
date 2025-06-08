using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Triangulos.Models;

namespace Triangulos.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }


        [HttpPost]
    public IActionResult Index(Triangulo triangulo)
    {
        if (!ModelState.IsValid)
        {
            return View(triangulo);
        }
        // Procesar el modelo válido...
        return View(triangulo);
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
