using Microsoft.AspNetCore;
// autogenerado por copilot
using Microsoft.AspNetCore.Mvc;
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

app.MapPost("/log", ([FromForm] double a) =>
{
    if (a <= 0 || a == 1)
    {
        var error = new
        {
            Error = "La base 'a' debe ser mayor que 0 y diferente de 1."
        };
        return Results.BadRequest(error);
    }

    var resultado = new Logaritmica
    {
        Funcion = $"f(x) = logₐ(x), a = {a}",
        Monotonia = a > 1 ? "creciente" : "decreciente",
        Inversa = $"f⁻¹(x) = aˣ = {a}ˣ"//copilot autogenerado
    };

    var xmlSerializer = new System.Xml.Serialization.XmlSerializer(resultado.GetType());
    using var stringWriter = new System.IO.StringWriter();
    xmlSerializer.Serialize(stringWriter, resultado);
    return Results.Content(stringWriter.ToString(), "application/xml");
}).DisableAntiforgery(); //https://chatgpt.com/share/685b58be-8564-8003-b5af-051aacec16e8

app.MapGet("/saludo/{nombre?}", (string? nombre) => //Autogenerado con copilot
{
    if (!string.IsNullOrEmpty(nombre))
    {
        return Results.Ok(new { saludo = $"¡Hola {nombre}!" });
    }
    return Results.StatusCode(422);
});




app.Run();

public record Logaritmica
{
    // https://chatgpt.com/share/685b5797-7704-8003-acc2-22dbfd873f6b
    public string? Funcion { get; init; }
    public string? Monotonia { get; init; }
    public string? Inversa { get; init; }
}