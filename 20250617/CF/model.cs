using Microsoft.EntityFrameworkCore;

public class ModelContext : DbContext
{
    public ModelContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "jps.db");
    }

    public string DbPath { get; private set; }

    public DbSet<Sorteo> Sorteos { get; set; }
    public DbSet<Numero> Numeros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"Data Source={DbPath}");
    }

    public class Sorteo
    {
        public int Id { get; set; }
        public string? Product { get; set; }
        public DateTime DateHour { get; set; }

        public List<Numero> Numeros { get; set; } = new List<Numero>();
    }

    public class Numero
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public bool Num { get; set; }

        public Sorteo? Sorteo { get; set; }
    }


















