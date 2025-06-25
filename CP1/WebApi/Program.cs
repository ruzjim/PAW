using Microsoft.AspNetCore;

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
    };

    var xmlSerializer = new System.Xml.Serialization.XmlSerializer(resultado.GetType());
    using var stringWriter = new System.IO.StringWriter();
    xmlSerializer.Serialize(stringWriter, resultado);
    return Results.Content(stringWriter.ToString(), "application/xml");
});

app.Run();

public record Logaritmica
{
    public string Funcion { get; init; }
    public string Monotonia { get; init; }
}