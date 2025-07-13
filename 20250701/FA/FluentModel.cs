using Microsoft.EntityFrameworkCore;

public class FluentContext : DbContext
{
    public DbSet<CategoryFluent> Categories { get; set; } = null!;
    public DbSet<ProductFluent> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryFluent>().HasKey(c => c.ThisIsTheKey);
        modelBuilder.Entity<CategoryFluent>()
            .Property(category => category.Description)
            .HasColumnName("descript")
            .HasColumnType("ntext");

        modelBuilder.Entity<ProductFluent>()
            .Property(product => product.ProductName)
            .IsRequired();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbFile = "fluent.db";
        string path = Path.Combine(AppContext.BaseDirectory, dbFile);
        string connectionString = $"Data Source={path}";
        Console.WriteLine($"Fluent: {connectionString}");
        optionsBuilder.UseSqlite(connectionString);

    }
}

public class CategoryFluent
{

    public int ThisIsTheKey { get; set; }
    public string CategoryName { get; set; } = null!;
    public string? Description { get; set; }
    public virtual HashSet<ProductFluent> Product { get; set; } = new();
}


public class ProductFluent
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public int CategoryId { get; set; }
    public virtual CategoryFluent Category { get; set; } = null!;
}