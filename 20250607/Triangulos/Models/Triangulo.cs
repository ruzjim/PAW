using System.ComponentModel.DataAnnotations;

namespace Triangulos.Models
{
    public class Triangulo : IValidatableObject
    {
        [Required(ErrorMessage = "El lado A es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El lado A debe ser mayor que cero.")]
        public int? a { get; set; }

        [Required(ErrorMessage = "El lado B es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El lado B debe ser mayor que cero.")]
        public int? b { get; set; }

        [Required(ErrorMessage = "El lado C es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El lado C debe ser mayor que cero.")]
        public int? c { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (a.HasValue && b.HasValue && c.HasValue)
            {
                var lados = new[] { a.Value, b.Value, c.Value }.OrderBy(x => x).ToArray();

                if (lados[0] + lados[1] <= lados[2])
                {
                    yield return new ValidationResult(
                        "La suma de los dos lados menores debe ser mayor que el lado mayor (desigualdad triangular).",
                        new[] { "" }); // <- Asociado al modelo completo, no a un campo específico
                }
            }
        }

        public double? Perimetro => (a.HasValue && b.HasValue && c.HasValue) ? a + b + c : (double?)null;

        public double? Semiperimetro => (Perimetro.HasValue) ? Perimetro / 2 : (double?)null;

        public double? Area
        {
            get
            {
                if (a.HasValue && b.HasValue && c.HasValue)
                {
                    double s = (a.Value + b.Value + c.Value) / 2.0;
                    return Math.Sqrt(s * (s - a.Value) * (s - b.Value) * (s - c.Value));
                }
                return null;
            }
        }

        public string Tipo
        {
            get
            {
                if (a == b && b == c)
                {
                    return "Equilátero";
                }
                else if (a == b || b == c || a == c)
                {
                    return "Isósceles";
                }
                else
                {
                    return "Escaleno";
                }
            }
        }


        public double? Anguloα
        {
            get
            {
                if (a.HasValue && b.HasValue && c.HasValue)
                {
                    return Math.Acos((Math.Pow(b.Value, 2) + Math.Pow(c.Value, 2) - Math.Pow(a.Value, 2)) / (2 * b.Value * c.Value)) * (180 / Math.PI);
                }
                return null;
            }
        }

        public double? Anguloβ
        {
            get
            {
                if (a.HasValue && b.HasValue && c.HasValue)
                {
                    return Math.Acos((Math.Pow(a.Value, 2) + Math.Pow(c.Value, 2) - Math.Pow(b.Value, 2)) / (2 * a.Value * c.Value)) * (180 / Math.PI);
                }
                return null;
            }
        }

        public double? Anguloγ
        {
            get
            {
                if (a.HasValue && b.HasValue && c.HasValue)
                {
                    return Math.Acos((Math.Pow(a.Value, 2) + Math.Pow(b.Value, 2) - Math.Pow(c.Value, 2)) / (2 * a.Value * b.Value)) * (180 / Math.PI);
                }
                return null;
            }
        }

    }
}
