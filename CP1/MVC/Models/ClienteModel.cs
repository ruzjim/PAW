using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class ClienteModel
{
  public int Cedula { get; set; }
  public int Edad { get; set; }
  public string Nombre { get; set; }
  public string Correo { get; set; }
  public bool EsValido { get; set; }
}
