using Microsoft.EntityFrameworkCore;

public class ModelContext : DbContext
{
    public DbSet<Producto> Productos { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=test;Trusted_Connection=True;");
    }


}
public class Producto
{
    public int Id { get; set; }
    public string? Nombre { get; set; }

    public double Precio { get; set; }

    public bool Vencido { get; set; }

}

