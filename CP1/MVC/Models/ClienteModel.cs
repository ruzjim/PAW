using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class ClienteModel //anotations autogenerados con copilot, warning corregido pensando
{
  [RegularExpression(@"^\d{9}$", ErrorMessage = "La cédula debe tener exactamente 9 dígitos.")]
  public int Cedula { get; set; }

  [Range(18, 125, ErrorMessage = "La edad debe estar entre 18 y 125 años.")]
  public int Edad { get; set; }

  [StringLength(10, ErrorMessage = "El nombre no puede exceder los 10 caracteres.")]
  public string? Nombre { get; set; }
  [EmailAddress(ErrorMessage = "El correo electrónico debe contener el símbolo @.")]
  public string? Correo { get; set; }
  public bool EsValido { get; set; }
}
