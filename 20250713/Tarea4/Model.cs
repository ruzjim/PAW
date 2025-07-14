using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

public class MyDbContext : DbContext
{
    public DbSet<Provincia> Provincias { get; set; }
    public DbSet<Canton> Cantones { get; set; }
    public DbSet<Distrito> Distritos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Provincia>().Property(p => p.Nombre).HasColumnName("ProvinciaNombre");
        modelBuilder.Entity<Canton>().Property(c => c.Nombre).HasColumnName("CantonNombre");
        modelBuilder.Entity<Distrito>().Property(d => d.Nombre).HasColumnName("DistritoNombre");

        // Configuring relationships
        modelBuilder.Entity<Canton>()
           .HasOne(c => c.Provincia)
           .WithMany(p => p.Cantones)
           .HasForeignKey(c => c.ProvinciaFK);

        modelBuilder.Entity<Distrito>()
            .HasOne(d => d.Canton)
            .WithMany(c => c.Distritos)
            .HasForeignKey(d => d.CantonFK);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string folder = Path.Combine(Environment.CurrentDirectory, "data");
        Directory.CreateDirectory(folder);
        string dbFile = "CR.db";
        string path = Path.Combine(folder, dbFile);
        string connectionString = $"Data Source={path};";
        optionsBuilder.UseSqlite(connectionString);
    }
}

public class Provincia
{
    [Key]
    public int ProvinciaPK { get; set; }
    public string Nombre { get; set; } = null!;
    public virtual List<Canton> Cantones { get; set; } = new();
}

public class Canton
{
    [Key]
    public int CantonPK { get; set; }
    public string Nombre { get; set; } = null!;
    public int ProvinciaFK { get; set; }
    public virtual List<Distrito> Distritos { get; set; } = new();
    public virtual Provincia Provincia { get; set; } = null!;
}

public class Distrito
{
    [Key]
    public int DistritoPK { get; set; }
    public string Nombre { get; set; } = null!;
    public int CantonFK { get; set; }
    public virtual Canton Canton { get; set; } = null!;
}