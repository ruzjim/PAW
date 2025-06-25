using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Practica.Models;

namespace Practica.Controllers;

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

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Form()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [HttpPost]
    public IActionResult Form(FormModel model)
    {
        if (ModelState.IsValid)
        {
            ViewBag.form = true;
            return View(model); // Pasa el modelo validado a la vista
        }

        ViewBag.form = false; // Para evitar mostrar los datos si hay errores
        return View(model); // Vuelve a mostrar el formulario con errores
    }

}
