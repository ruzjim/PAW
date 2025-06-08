# Estudiante
Rusdwin Jimenez Morales

# Carné
FH15000251

# Enlace Github
https://github.com/ruzjim/PAW/tree/main/20250607

# Comandos de dotnet utilizados (CLI).
dotnet new sln -n Practica2
dotnet new mvc -o Triangulos
dotnet sln add Triangulos
dotnet run

# Paginas Web
no se utilizaron paginas de referencia

# Prompts
en el siguiente link están los prompts del IA
https://chatgpt.com/share/6845b13f-d404-8003-8a91-84a4ef0a4eb1


# Prompts
tengo un proyecto mvc creado de esta manera 

dotnet new sln -n Practica2
dotnet new mvc -o Triangulos
dotnet sln add Triangulos

asi como el laravel hay algun comando para crear un modelo o solo creo la clase manualmente?


ChatGPT Plus
En ASP.NET Core MVC (como el proyecto que estás creando con dotnet new mvc), no hay un comando incorporado como en Laravel para crear modelos automáticamente, al estilo de php artisan make:model.

Opciones para crear modelos en ASP.NET Core MVC:
Manual (recomendado en muchos casos)
Crea tu modelo manualmente agregando una clase dentro de una carpeta Models:

bash
Copiar
Editar
mkdir Triangulos/Models
Luego crea un archivo, por ejemplo, Triangulo.cs:

csharp
Copiar
Editar
// Triangulos/Models/Triangulo.cs
namespace Triangulos.Models
{
    public class Triangulo
    {
        public int LadoA { get; set; }
        public int LadoB { get; set; }
        public int LadoC { get; set; }

        public string Tipo()
        {
            if (LadoA == LadoB && LadoB == LadoC)
                return "Equilátero";
            if (LadoA == LadoB || LadoB == LadoC || LadoA == LadoC)
                return "Isósceles";
            return "Escaleno";
        }
    }
}
Usar herramientas de scaffolding (si estás usando EF Core)
Si planeas usar Entity Framework Core (como si tu modelo representa una tabla), puedes usar dotnet-aspnet-codegenerator. Primero instálalo si no lo tienes:

bash
Copiar
Editar
dotnet tool install --global dotnet-aspnet-codegenerator
Luego puedes usar algo como:

bash
Copiar
Editar
dotnet aspnet-codegenerator model -name Triangulo -outDir Models
Esto crea una clase vacía llamada Triangulo en la carpeta Models, pero no genera lógica interna como Laravel. Es más útil cuando estás generando controladores y vistas desde un modelo (scaffold).

En resumen
No, ASP.NET Core MVC no tiene un comando directo como php artisan make:model que cree un modelo con toda la funcionalidad esperada. Se espera que crees clases manualmente o uses scaffolding para acelerar parte del trabajo (especialmente si usas Entity Framework).

¿Quieres que te ayude a crear un modelo y conectarlo a una vista/controlador para probarlo?







Tú dijiste:
donde consideras mejor aplicar esto? en el form en el modelo o en el controlador?

La suma de los dos valores menores debe ser mayor que el tercer valor. Esto es para verificar que cumple con la desigualdad triángular. Esta validación la puede ejecutar en la capa que considere oportuna.


ChatGPT Plus
Excelente pregunta. La validación de la desigualdad triangular (la suma de los dos lados menores debe ser mayor que el mayor) es una regla de negocio, y por tanto, la mejor práctica es aplicarla en el modelo o en una clase de servicio o validador que forme parte de la lógica de negocio.

Comparativa por capa:
✅ Modelo (mejor opción)
✔ Centraliza la lógica del dominio.

✔ Reutilizable en cualquier parte (controlador, servicios, pruebas).

✔ Se alinea con los principios de diseño limpio (Clean Architecture / DDD).

Ejemplo:

csharp
Copiar
Editar
public class Triangulo
{
    public int LadoA { get; set; }
    public int LadoB { get; set; }
    public int LadoC { get; set; }

    public bool EsValido()
    {
        var lados = new[] { LadoA, LadoB, LadoC }.OrderBy(x => x).ToArray();
        return lados[0] + lados[1] > lados[2];
    }

    public string Tipo()
    {
        if (!EsValido()) return "No es un triángulo válido";

        if (LadoA == LadoB && LadoB == LadoC)
            return "Equilátero";
        if (LadoA == LadoB || LadoB == LadoC || LadoA == LadoC)
            return "Isósceles";
        return "Escaleno";
    }
}
🟡 Controlador
Puede funcionar, pero no es ideal:

❌ Repetitivo si necesitas validar en varios lugares.

❌ Mezcla lógica de negocio con lógica de presentación o flujo.

🔴 Formulario (vista / cliente)
Útil como validación previa para UX, pero no es suficiente ni confiable.

❌ Puede ser manipulado fácilmente.

✔ Puede usarse adicionalmente con JavaScript para prevenir errores tempranos, pero no reemplaza la validación del lado del servidor.

Recomendación final:
Haz la validación en el modelo o en una clase de dominio/servicio, y si quieres, complementa con validación visual en el formulario y validación redundante en el controlador antes de proceder (defensivo).

¿Quieres que te ayude a integrar esta lógica con un formulario y controlador?







Tú dijiste:
este es mi modelo como debo de modificarlo?

using System.ComponentModel.DataAnnotations;

namespace Triangulos.Models
{
    public class Triangulo
    {
        [Required(ErrorMessage = "El lado A es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El lado a debe ser mayor que cero.")]
        public int? a { get; set; }

        [Required(ErrorMessage = "El lado B es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El lado b debe ser mayor que cero.")]
        public int? b { get; set; }

        [Required(ErrorMessage = "El lado C es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El lado c debe ser mayor que cero.")]
        public int? c { get; set; }
    }
}


ChatGPT Plus
Tu modelo está bien estructurado en cuanto a validaciones básicas con DataAnnotations, pero para aplicar la validación de la desigualdad triangular, lo más limpio y reutilizable es usar una validación personalizada.

Hay dos formas principales de hacerlo:

✅ Opción recomendada: Validación personalizada con IValidatableObject
Modifica tu modelo para implementar la interfaz IValidatableObject, que permite definir validaciones personalizadas a nivel de modelo.

csharp
Copiar
Editar
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
            // Solo validar si todos los lados tienen valor
            if (a.HasValue && b.HasValue && c.HasValue)
            {
                var lados = new[] { a.Value, b.Value, c.Value }.OrderBy(x => x).ToArray();

                if (lados[0] + lados[1] <= lados[2])
                {
                    yield return new ValidationResult(
                        "La suma de los dos lados menores debe ser mayor que el lado mayor (desigualdad triangular).",
                        new[] { nameof(a), nameof(b), nameof(c) });
                }
            }
        }
    }
}
¿Qué hace esto?
Validate se ejecuta automáticamente durante la validación del modelo (por ejemplo, en el ModelState.IsValid en el controlador).

Asegura que no se intente validar si algún lado es null.

Ordena los lados y valida la desigualdad triangular.

¿Y en el controlador?
Con esto, en tu controlador basta con validar normalmente:

csharp
Copiar
Editar
[HttpPost]
public IActionResult Index(Triangulo triangulo)
{
    if (!ModelState.IsValid)
    {
        return View(triangulo); // Muestra errores de validación
    }

    // Todo OK, puedes calcular tipo, mostrar resultado, etc.
    return View("Resultado", triangulo);
}
¿Quieres también que te ayude a mostrar el tipo de triángulo o diseñar la vista/formulario?







Tú dijiste:
eso no me gusta porque pone el error en los 3 input este es mi html como pueso hacer para que solo ponga el error de desigualdad debajo de resultados

@model Triangulos.Models.Triangulo
@{
    ViewData["Title"] = "Triangulos";
}

<div class="text-center">
    <div class=" row justify-content-center">
        <div class="col-md-6 p-4">
            <h1 class="display-4">Triangulos</h1>
            <p class="lead">Aplicación para calcular informacion de un triángulo.</p>
            <form method="post" asp-action="Index">
                <h5>Ingrese las longitudes de los lados del triángulo:</h5>
                <div class="mb-3">
                    <div class="form-floating mb-3">
                        <input asp-for="a" class="form-control" placeholder="Lado a" />
                        <label asp-for="a"></label>
                        <span asp-validation-for="a" class="text-danger"></span>
                    </div>
                </div>
                <div class="mb-3">
                    <div class="form-floating mb-3">
                        <input asp-for="b" class="form-control" placeholder="Lado b" />
                        <label asp-for="b"></label>
                        <span asp-validation-for="b" class="text-danger"></span>
                    </div>
                </div>
                <div class="mb-3">
                    <div class="form-floating mb-3">
                        <input asp-for="c" class="form-control" placeholder="Lado c" />
                        <label asp-for="c"></label>
                        <span asp-validation-for="c" class="text-danger"></span>
                    </div>
                </div>
                <button type="submit" class="btn btn-primary">Calcular</button>
            </form>
        </div>

        <div class="col-md-6 p-4">
            
            <div class="card">
                <div class="card-header">
                    <h2 class="display-6">Resultados</h2>
                </div>
                <div class="card-body">
                    <p id="resultado" class="card-text">Aquí aparecerá el resultado del cálculo.</p>
                </div>
            </div>

        </div>
    </div>


</div>


ChatGPT Plus
Perfecto, gracias por compartir tu vista. Para mostrar el mensaje de la desigualdad triangular solamente debajo de la sección de resultados (y no debajo de los inputs), debes hacer lo siguiente:

✅ 1. En el modelo
Modifica la validación para no asociar el error a propiedades individuales, sino al modelo completo (pasando null o un string.Empty en lugar del array new[] { nameof(a), nameof(b), nameof(c) }).

csharp
Copiar
Editar
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
✅ 2. En la vista
En la parte donde tienes el resultado (dentro del card-body), añade este bloque Razor para mostrar errores generales (no ligados a campos específicos):

cshtml
Copiar
Editar
@if (!ViewData.ModelState.IsValid)
{
    <div class="text-danger">
        @Html.ValidationSummary(false, "", new { @class = "text-danger" })
    </div>
}
Colócalo dentro del card-body, donde está el resultado:

cshtml
Copiar
Editar
<div class="card-body">
    @if (!ViewData.ModelState.IsValid)
    {
        <div class="text-danger">
            @Html.ValidationSummary(false, "", new { @class = "text-danger" })
        </div>
    }

    <p id="resultado" class="card-text">Aquí aparecerá el resultado del cálculo.</p>
</div>
✅ Resultado
Las validaciones [Required] y [Range] seguirán mostrándose debajo de cada input.

El mensaje de desigualdad triangular aparecerá solo en el área de resultados cuando sea necesario.

¿Quieres también mostrar el tipo de triángulo (Equilátero, Isósceles, etc.) cuando la validación es correcta?







Tú dijiste:
entendido quedo muy bien pero ahora si dejo un campo vacio me muestra el error en el espacio de resultados y quisiera que esos solo se muestren debajo de los input


ChatGPT Plus
Perfecto, y muy buena observación 👌

El problema está en que el ValidationSummary que usamos para mostrar el error de desigualdad triangular también está mostrando los errores de validación por campo (como los [Required]), porque por defecto ValidationSummary(false) incluye todos los errores no asociados directamente a campos válidos (y los campos vacíos a veces se validan así).

✅ Solución
Cambia esta línea:

cshtml
Copiar
Editar
@Html.ValidationSummary(false, "", new { @class = "text-danger" })
por esta:

cshtml
Copiar
Editar
@Html.ValidationSummary(true, "", new { @class = "text-danger" })
🔍 El primer parámetro (true) significa: solo mostrar errores del modelo no asociados a propiedades individuales (justo lo que queremos).

Resultado:
Errores de campos vacíos ([Required], [Range]) solo aparecerán debajo de los inputs, como siempre.

El error de la desigualdad triangular (el que pusimos con new[] { "" }) aparecerá únicamente en la sección de resultados.

Código actualizado (por si acaso):
cshtml
Copiar
Editar
@if (!ViewData.ModelState.IsValid)
{
    <div class="text-danger">
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
    </div>
}
¿Quieres que ahora mostremos también el tipo de triángulo (Equilátero, etc.) en esa misma tarjeta de resultados cuando la validación es válida?







Tú dijiste:
exelente lo entendi perfecto y funciona bien


ChatGPT Plus
¡Me alegra muchísimo saberlo! 🙌
Tu proyecto va muy bien encaminado, y estás aplicando muy buenas prácticas desde el inicio.

Si en algún momento quieres:

Mostrar el tipo de triángulo automáticamente,

Añadir validación con JavaScript en el cliente,

Guardar datos en una base de datos con Entity Framework,

O incluso desplegarlo en producción,

estaré encantado de ayudarte con eso paso a paso.

¡Mucho éxito con tu aplicación de triángulos! 🟦📐
¿Quieres que sigamos con algo más?







Tú dijiste:
agregue esto al modelo
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

como hago que debajo de resultados se muestren esos datos?


ChatGPT Plus
¡Muy bien! Ya agregaste las propiedades calculadas Perimetro, Semiperimetro y Area en el modelo, ahora solo falta mostrarlas en la sección de resultados de la vista.

Te muestro cómo hacerlo:

✅ 1. Asegúrate de estar pasando el modelo a la vista en el POST
En tu controlador, al final del POST Index, deberías tener algo así:

csharp
Copiar
Editar
[HttpPost]
public IActionResult Index(Triangulo triangulo)
{
    if (!ModelState.IsValid)
    {
        return View(triangulo);
    }

    // Si todo está bien, se vuelve a mostrar el modelo con resultados
    return View(triangulo);
}
✅ 2. En la vista, dentro de la tarjeta de resultados
Reemplaza el contenido actual de este bloque:

html
Copiar
Editar
<p id="resultado" class="card-text">Aquí aparecerá el resultado del cálculo.</p>
por algo como esto:

cshtml
Copiar
Editar
@if (Model != null && Model.a.HasValue && Model.b.HasValue && Model.c.HasValue && ViewData.ModelState.IsValid)
{
    <p><strong>Perímetro:</strong> @Model.Perimetro</p>
    <p><strong>Semiperímetro:</strong> @Model.Semiperimetro</p>
    <p><strong>Área:</strong> @Model.Area</p>
    <p><strong>Tipo:</strong> @Model.Tipo()</p>
}
else
{
    <p id="resultado" class="card-text">Aquí aparecerá el resultado del cálculo.</p>
}
✅ Este bloque verifica que los lados están presentes y que el modelo es válido antes de mostrar los resultados.

✅ 3. Agrega el método Tipo() en el modelo (si aún no lo tienes):
csharp
Copiar
Editar
public string Tipo()
{
    if (!EsValido()) return "No es un triángulo válido";

    if (a == b && b == c)
        return "Equilátero";
    if (a == b || b == c || a == c)
        return "Isósceles";
    return "Escaleno";
}
Y mantén también el método EsValido() que ya tenías para validar la desigualdad triangular.

✅ Resultado final
Cuando el usuario ingresa valores válidos:

Se muestran el perímetro, semiperímetro, área y el tipo de triángulo.

Si hay errores, se muestran solo los mensajes correspondientes (como ya lo tienes bien configurado).

¿Quieres que formateemos el área o el perímetro con solo dos decimales?







Tú dijiste:
como debe ser esto
@if (!ViewData.ModelState.IsValid)
                    {
                        <div class="text-danger">
                            @Html.ValidationSummary(true, "", new { @class = "text-danger" })
                        </div>
                    }

                    else if (Model != null && Model.a.HasValue && Model.b.HasValue && Model.c.HasValue &&
                                        ViewData.ModelState.IsValid)
                    {
                        <p><strong>Perímetro:</strong> @Model.Perimetro</p>
                        <p><strong>Semiperímetro:</strong> @Model.Semiperimetro</p>
                        <p><strong>Área:</strong> @Model.Area</p>
                        <p><strong>Tipo:</strong> @Model.Tipo</p>
                    }
                    else
                    {
                        <p id="resultado" class="card-text">Aquí aparecerá el resultado del cálculo.</p>
                    }


ChatGPT Plus
¡Muy bien! Estás casi perfecto con la estructura, solo hay un pequeño detalle técnico:

En Razor, no puedes usar directamente estructuras como if { ... } else if { ... } else { ... } cuando dentro de los bloques hay HTML sin @: o bloques explícitos @{ ... }. Esto genera errores o comportamiento inesperado.

✅ Solución correcta
Debes dividir los bloques en múltiples @if, y dejar que Razor los procese de forma independiente.

Aquí tienes la forma correcta y limpia:

cshtml
Copiar
Editar
@if (!ViewData.ModelState.IsValid)
{
    <div class="text-danger">
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
    </div>
}
else if (Model != null && Model.a.HasValue && Model.b.HasValue && Model.c.HasValue && ViewData.ModelState.IsValid)
{
    <p><strong>Perímetro:</strong> @Model.Perimetro?.ToString("0.00")</p>
    <p><strong>Semiperímetro:</strong> @Model.Semiperimetro?.ToString("0.00")</p>
    <p><strong>Área:</strong> @Model.Area?.ToString("0.00")</p>
    <p><strong>Tipo:</strong> @Model.Tipo()</p>
}
else
{
    <p id="resultado" class="card-text">Aquí aparecerá el resultado del cálculo.</p>
}
🛠 Detalles importantes
Uso de ?.ToString("0.00"): Para mostrar los resultados con dos decimales solo si tienen valor.

@Model.Tipo() (con paréntesis): Asegúrate que sea un método, no una propiedad.

ValidationSummary(true, ...): Ya lo tienes bien, solo errores del modelo (como la desigualdad triangular) aparecerán ahí.

¿Te gustaría que los resultados también se actualicen automáticamente con JavaScript sin recargar la página?







Tú dijiste:
ahora debo calcular los valores de los ángulos del triángulo, denominados por las letras griegas alfa (α), beta (β) y gamma (ɣ).


ChatGPT Plus
Perfecto. Para calcular los ángulos internos de un triángulo (α, β, ɣ) dados los tres lados (a, b, c), puedes usar la Ley del Coseno:

🧮 Fórmulas (Ley del Coseno)
α = acos[(b² + c² − a²) / (2bc)]

β = acos[(a² + c² − b²) / (2ac)]

ɣ = acos[(a² + b² − c²) / (2ab)]

📌 Los resultados están en radianes, pero puedes convertirlos a grados multiplicando por 180 / Math.PI.

✅ Paso 1: Agregar las propiedades al modelo
En tu clase Triangulo, agrega estas propiedades:

csharp
Copiar
Editar
public double? Alfa
{
    get
    {
        if (a.HasValue && b.HasValue && c.HasValue && EsValido())
        {
            return Math.Acos((Math.Pow(b.Value, 2) + Math.Pow(c.Value, 2) - Math.Pow(a.Value, 2)) / (2 * b.Value * c.Value)) * (180 / Math.PI);
        }
        return null;
    }
}

public double? Beta
{
    get
    {
        if (a.HasValue && b.HasValue && c.HasValue && EsValido())
        {
            return Math.Acos((Math.Pow(a.Value, 2) + Math.Pow(c.Value, 2) - Math.Pow(b.Value, 2)) / (2 * a.Value * c.Value)) * (180 / Math.PI);
        }
        return null;
    }
}

public double? Gamma
{
    get
    {
        if (a.HasValue && b.HasValue && c.HasValue && EsValido())
        {
            return Math.Acos((Math.Pow(a.Value, 2) + Math.Pow(b.Value, 2) - Math.Pow(c.Value, 2)) / (2 * a.Value * b.Value)) * (180 / Math.PI);
        }
        return null;
    }
}
✅ Paso 2: Mostrar los ángulos en la vista
En la parte de Resultados, añade esto donde estás mostrando perímetro, área y tipo:

cshtml
Copiar
Editar
<p><strong>Ángulo α (°):</strong> @Model.Alfa?.ToString("0.00")</p>
<p><strong>Ángulo β (°):</strong> @Model.Beta?.ToString("0.00")</p>
<p><strong>Ángulo ɣ (°):</strong> @Model.Gamma?.ToString("0.00")</p>
🧠 Extra: Verificación opcional
Si deseas estar seguro de que la suma da 180°, puedes añadir esta propiedad (opcional):

csharp
Copiar
Editar
public double? SumaAngulos => Alfa + Beta + Gamma;
Y en la vista:

cshtml
Copiar
Editar
<p><strong>Suma de ángulos:</strong> @Model.SumaAngulos?.ToString("0.00") °</p>
¿Quieres que te ayude a mostrar los ángulos visualmente o en un gráfico con SVG o Canvas?