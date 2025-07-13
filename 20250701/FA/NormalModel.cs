using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


public class NormalContext : DbContext
{
    public DbSet<CategoryNormal> Categories { get; set; } = null!;
    public DbSet<ProductNormal> Products { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbFile = "normal.db";
        string path = Path.Combine(AppContext.BaseDirectory, dbFile);
        string connectionString = $"Data Source={path}";
        Console.WriteLine($"Normal: {connectionString}");
        optionsBuilder.UseSqlite(connectionString);

    }
}

public class CategoryNormal
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;

    [Column("descript", TypeName = "ntext")]
    public string? Description { get; set; }

    public virtual HashSet<ProductNormal> Product { get; set; } = new();
}


public class ProductNormal
{
    public int Id { get; set; }
    [Required]
    public string ProductName { get; set; } = null!;
    public int CategoryId { get; set; }

    public virtual CategoryNormal Category { get; set; } = null!;
}