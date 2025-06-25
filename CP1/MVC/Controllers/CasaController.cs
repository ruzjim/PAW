using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class CasaController : Controller
{
    [HttpGet]
    public IActionResult Inicio()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Formulario()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Formulario(ClienteModel cliente)
    {
        cliente.EsValido = ModelState.IsValid;
        return View(cliente);
    }

    [HttpGet]
    public IActionResult Identificacion(int id)
    {
        ViewBag.Id = id;
        return View("Identificacion");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
