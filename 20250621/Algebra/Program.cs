var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Redirect("/swagger/index.html"));


app.MapGet("/lineal", (
    double? b,
    double? m,
    double? x1,
    double? y1,
    double? x2,
    double? y2,
    bool xml = false) =>
{
    if (b == null)
        return Results.BadRequest(new { error = "El parámetro 'b' es obligatorio." });

    // Validar que se tenga m o ambos puntos (x1, y1, x2, y2)
    bool tieneM = m != null;
    bool tienePuntos = x1 != null && y1 != null && x2 != null && y2 != null && x2 != x1;

    if (!tieneM && !tienePuntos)
    {
        return Results.BadRequest(new { error = "Debe proveer el valor de 'm' o los cuatro valores 'x1', 'y1', 'x2', 'y2' (con x1 != x2)." });
    }

    double pendiente;

    if (tieneM)
    {
        pendiente = m.Value;
    }
    else
    {
        pendiente = (y2.Value - y1.Value) / (x2.Value - x1.Value);
    }

    // funcion: f(x) = mx + b
    string funcion = $"f(x) = {pendiente}x + {b}";

    // pendiente: creciente o decreciente
    string tipoPendiente = pendiente > 0 ? "creciente" : pendiente < 0 ? "decreciente" : "constante";

    // interseccionConEjeX: x = -b/m, (x, 0)
    string interseccionX;
    if (pendiente != 0)
    {
        double x = -b.Value / pendiente;
        interseccionX = $"({x}, 0)";
    }
    else
    {
        interseccionX = "(indefinido, 0)";
    }

    // interseccionConEjeY: (0, b)
    string interseccionY = $"(0, {b})";

    // Para XML, usar una clase con propiedades con nombres correctos
    if (xml)
    {
        var resultadoXml = new Lineal
        {
            Funcion = funcion,
            Pendiente = tipoPendiente,
            InterseccionConEjeX = interseccionX,
            InterseccionConEjeY = interseccionY
        };
        var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Lineal));
        using var stringWriter = new System.IO.StringWriter();
        xmlSerializer.Serialize(stringWriter, resultadoXml);
        return Results.Content(stringWriter.ToString(), "application/xml");
    }
    else
    {
        var resultado = new Dictionary<string, string>
        {
            { "funcion", funcion },
            { "pendiente", tipoPendiente },
            { "interseccionConEjeX", interseccionX },
            { "interseccionConEjeY", interseccionY }
        };
        return Results.Json(resultado);
    }
});

app.MapGet("/cuad", (
    double? a,
    double? b,
    double? c,
    bool xml = false) =>
{
    if (a == null || b == null || c == null)
        return Results.BadRequest(new { error = "Los parámetros 'a', 'b' y 'c' son obligatorios." });
    if (a == 0)
        return Results.BadRequest(new { error = "El parámetro 'a' debe ser diferente de cero." });

    double aVal = a.Value;
    double bVal = b.Value;
    double cVal = c.Value;

    string funcion = $"f(x) = {aVal}x² + {bVal}x + {cVal}";

    double discriminante = bVal * bVal - 4 * aVal * cVal;
    string discriminanteStr = $"Δ = {discriminante}";

    double ejeSimetria = -bVal / (2 * aVal);
    string ejeSimetriaStr = $"x = {ejeSimetria}";

    string concavidad = aVal > 0 ? "hacia arriba" : "hacia abajo";

    double verticeY = aVal * ejeSimetria * ejeSimetria + bVal * ejeSimetria + cVal;
    string vertice = $"({ejeSimetria}, {verticeY})";

    string interseccionX;
    if (discriminante < 0)
    {
        interseccionX = "no hay";
    }
    else if (discriminante == 0)
    {
        double x = -bVal / (2 * aVal);
        interseccionX = $"({x}, 0)";
    }
    else
    {
        double sqrtD = Math.Sqrt(discriminante);
        double x1 = (-bVal + sqrtD) / (2 * aVal);
        double x2 = (-bVal - sqrtD) / (2 * aVal);
        interseccionX = $"({x1}, 0) y ({x2}, 0)";
    }

    string interseccionY = $"(0, {cVal})";

    string ambito;
    if (aVal > 0)
        ambito = $"[{verticeY}, +∞[";
    else
        ambito = $"]-∞, {verticeY}]";

    string monotonias;
    if (aVal > 0)
        monotonias = $"crece en ]{ejeSimetria}, +∞[ y decrece en ]-∞, {ejeSimetria}[";
    else
        monotonias = $"crece en ]-∞, {ejeSimetria}[ y decrece en ]{ejeSimetria}, +∞[";

    if (xml)
    {
        var resultadoXml = new Cuadratica
        {
            Funcion = funcion,
            Discriminante = discriminanteStr,
            EjeDeSimetria = ejeSimetriaStr,
            Concavidad = concavidad,
            Vertice = vertice,
            InterseccionConEjeX = interseccionX,
            InterseccionConEjeY = interseccionY,
            Ambito = ambito,
            Monotonias = monotonias
        };
        var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Cuadratica));
        using var stringWriter = new System.IO.StringWriter();
        xmlSerializer.Serialize(stringWriter, resultadoXml);
        return Results.Content(stringWriter.ToString(), "application/xml");
    }
    else
    {
        var resultado = new Dictionary<string, string>
        {
            { "funcion", funcion },
            { "discriminante", discriminanteStr },
            { "ejeDeSimetria", ejeSimetriaStr },
            { "concavidad", concavidad },
            { "vertice", vertice },
            { "interseccionConEjeX", interseccionX },
            { "interseccionConEjeY", interseccionY },
            { "ambito", ambito },
            { "monotonias", monotonias }
        };
        return Results.Json(resultado);
    }
});

app.MapGet("/exp", (
    double? b,
    bool xml = false) =>
{
    if (b == null || b <= 0 || b == 1)
    {
        return Results.BadRequest(new { error = "La base 'b' debe ser mayor que 0 y diferente de 1." });
    }

    string funcion = $"f(x) = {b}ˣ";
    string monotonia = b > 1 ? "creciente" : "decreciente";

    if (xml)
    {
        var resultadoXml = new Exponencial
        {
            Funcion = funcion,
            Monotonia = monotonia
        };
        var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Exponencial));
        using var stringWriter = new System.IO.StringWriter();
        xmlSerializer.Serialize(stringWriter, resultadoXml);
        return Results.Content(stringWriter.ToString(), "application/xml");
    }
    else
    {
        var resultado = new Dictionary<string, string>
        {
            { "funcion", funcion },
            { "monotonia", monotonia }
        };
        return Results.Json(resultado);
    }
});

app.Run();


// Clase para serialización XML
public class Lineal
{
    public string? Funcion { get; set; }
    public string? Pendiente { get; set; }
    public string? InterseccionConEjeX { get; set; }
    public string? InterseccionConEjeY { get; set; }
}

// Clase para serialización XML
public class Cuadratica
{
    public string? Funcion { get; set; }
    public string? Discriminante { get; set; }
    public string? EjeDeSimetria { get; set; }
    public string? Concavidad { get; set; }
    public string? Vertice { get; set; }
    public string? InterseccionConEjeX { get; set; }
    public string? InterseccionConEjeY { get; set; }
    public string? Ambito { get; set; }
    public string? Monotonias { get; set; }
}

public class Exponencial
{
    public string? Funcion { get; set; }
    public string? Monotonia { get; set; }
}