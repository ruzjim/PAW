using Microsoft.EntityFrameworkCore;

public class LlenarBd
{
    public static void LlenarBaseDeDatos()
    {
        using var context = new MyDbContext();

        Console.Write("Procesando...");
        string csvPath = Path.Combine(Environment.CurrentDirectory, "data", "CR.csv");

        if (!File.Exists(csvPath))
        {
            Console.WriteLine("El archivo CSV no existe.");
            return;
        }

        var lineas = File.ReadAllLines(csvPath);
        foreach (var linea in lineas.Skip(1)) // Saltar encabezados
        {
            var campos = linea.Split(',');
            if (campos.Length < 3) continue;

            string nombreProvincia = campos[0].Trim();
            string nombreCanton = campos[1].Trim();
            string nombreDistrito = campos[2].Trim();

            // Buscar o agregar provincia
            var provincia = context.Provincias
                .Include(p => p.Cantones)
                .FirstOrDefault(p => p.Nombre == nombreProvincia);

            if (provincia == null)
            {
                provincia = new Provincia { Nombre = nombreProvincia };
                context.Provincias.Add(provincia);
                context.SaveChanges(); // Necesario para obtener la PK
            }

            // Buscar o agregar cantón
            var canton = context.Cantones
                .Include(c => c.Distritos)
                .FirstOrDefault(c => c.Nombre == nombreCanton && c.ProvinciaFK == provincia.ProvinciaPK);

            if (canton == null)
            {
                canton = new Canton { Nombre = nombreCanton, ProvinciaFK = provincia.ProvinciaPK };
                context.Cantones.Add(canton);
                context.SaveChanges();
            }

            // Buscar o agregar distrito
            var distrito = context.Distritos
                .FirstOrDefault(d => d.Nombre == nombreDistrito && d.CantonFK == canton.CantonPK);

            if (distrito == null)
            {
                distrito = new Distrito { Nombre = nombreDistrito, CantonFK = canton.CantonPK };
                context.Distritos.Add(distrito);
            }
        }

        context.SaveChanges();

        Console.WriteLine(" Listo.");
    }
}