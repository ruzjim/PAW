using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class ListaModel : PageModel
    {
            public IEnumerable<string> MiLista { get; set; }
        public void OnGet()
        {
            ViewData["Title"] = "Lista de Elementos";
            MiLista = new List<string>
            {
                "Elemento 1",
                "Elemento 2",
                "Elemento 3"
            };
        }
    }
}
