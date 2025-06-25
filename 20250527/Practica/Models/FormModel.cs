namespace Practica.Models
{
    using System.ComponentModel.DataAnnotations;
    public class FormModel
    {
        [Range(1, 1000, ErrorMessage = "El Id debe estar entre 1 y 1000")]
        [Required(ErrorMessage = "El Id es obligatorio")]
        public int Id { get; set; }
        [MinLength(10, ErrorMessage = "El Nombre debe tener al menos 10 caracteres")]
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string? Nombre { get; set; }

        [EmailAddress(ErrorMessage = "El Correo no es válido")]
        [Required(ErrorMessage = "El Correo es obligatorio")]
        public string? Correo { get; set; }

        [Range(18, 100, ErrorMessage = "La Edad debe estar entre 18 y 100")]
        [Required(ErrorMessage = "La Edad es obligatoria")]
        public int Edad { get; set; }
    }
}
