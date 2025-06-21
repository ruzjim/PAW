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

var todosItems = new List<string>();

var todosApi = app.MapGroup("/todos");

todosApi.MapGet("/", () => todosItems);

todosApi.MapGet("/{index:int}", (int index) =>
{
    if (index < 0 || index >= todosItems.Count)
    {
        return Results.NotFound();
    }
    else 
    {
        return Results.Ok(todosItems[index]);
    }
});

todosApi.MapPost("/add", ([FromForm] string item) =>
{
    todosItems.Add(item);
    return Results.Created($"/todos/{item}", item);
}).DisableAntiforgery();

todosApi.MapPut("/update/{index}", (int index, [FromForm] string item) =>
{
    if (index < 0 || index >= todosItems.Count)
    {
        return Results.NotFound();
    }
    else
    {
        todosItems[index] = item;
        return Results.Ok(item);
    }
}).DisableAntiforgery(); 

todosApi.MapDelete("/delete/{index}", (int index) =>
{
    if (index < 0 || index >= todosItems.Count)
    {
        return Results.NotFound();
    }
    else
    {
        todosItems.RemoveAt(index);
        return Results.NoContent();
    }
}).DisableAntiforgery();

var tareas = new List<TodoItem>();

var tareasApi = app.MapGroup("/tareas");

tareasApi.MapGet("/", (HttpContext http) =>
{         //Ojo cambiar headers Accept = application/xml
    var accept = http.Request.Headers["Accept"].ToString();
    if (accept.Contains("application/xml"))
    {
        http.Response.ContentType = "application/xml";
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<TodoItem>));
        var ms = new MemoryStream();
        serializer.Serialize(ms, tareas);
        ms.Position = 0;
        return Results.File(ms, "application/xml");
    }
    return Results.Ok(tareas);
});

tareasApi.MapGet("/{index:int}", (int index) =>
{
    if (index < 0 || index >= tareas.Count)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(tareas[index]);
    }
});

tareasApi.MapPost("/add", ([FromForm] string description, [FromForm] bool isDone = false) =>
{
    var item = new TodoItem { Description = description, IsDone = isDone };
    tareas.Add(item);
    return Results.Created($"/tareas/{item.Description}", item);
}).DisableAntiforgery();

tareasApi.MapPut("/update/{index}", (int index, [FromForm] bool isDone) =>
{
    if (index < 0 || index >= tareas.Count)
    {
        return Results.NotFound();
    }
    else
    {
        tareas[index].IsDone = isDone;
        return Results.Ok(tareas[index]);
    }
}).DisableAntiforgery();

tareasApi.MapDelete("/delete/{index}", (int index) =>
{
    if (index < 0 || index >= tareas.Count)
    {
        return Results.NotFound();
    }
    else
    {
        tareas.RemoveAt(index);
        return Results.NoContent();
    }
}).DisableAntiforgery();

app.Run();
