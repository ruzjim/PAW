using Microsoft.EntityFrameworkCore;

using var db = new ModelContext();
db.Database.EnsureCreated();

Console.WriteLine("Database path: " + db.DbPath);

Console.WriteLine("Dijite el nombre del producto:");
var producto = Console.ReadLine();

var sorteo = new Sorteo
{
    Product = producto,
    DateHour = DateTime.Now
};

db.Sorteos.Add(sorteo);
await db.SaveChangesAsync();
