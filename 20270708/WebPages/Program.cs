var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapRazorPages();
app.MapGet("/", () => "Hello World!");

app.Run();



// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();

// builder.Services.AddRazorPages();

// app.UseDefaultFiles();
// app.UseStaticFiles();

// app.MapGet("/", () => "Hello World!");

// app.MapRazorPages();
// app.Run();
