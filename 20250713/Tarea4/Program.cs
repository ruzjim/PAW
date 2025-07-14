using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

using var context = new MyDbContext();

bool hayProvincias = context.Provincias.Any();

if (!hayProvincias)
{
    Console.WriteLine("La base de datos está vacía, por lo que debe ser llenada a partir de los datos del archivo CSV.");
    LlenarBd.LlenarBaseDeDatos();
}
else
{
    Console.WriteLine("Provincias:\n");
    var provincias = context.Provincias.ToList();
    foreach (var provincia in provincias)
    {
        Console.WriteLine($"{provincia.ProvinciaPK}. {provincia.Nombre}");
    }

    Console.Write("\n> Indique la provincia: ");
    string? provinciaElegida = Console.ReadLine();

    if (int.TryParse(provinciaElegida, out int provinciaId))
    {
        var provincia = context.Provincias
        .Include(p => p.Cantones) // Importante: incluir cantones
        .FirstOrDefault(p => p.ProvinciaPK == provinciaId);

        if (provincia != null)
        {
            Console.WriteLine($"\nCantones de {provincia.Nombre}:\n");

            foreach (var canton in provincia.Cantones)
            {
                Console.WriteLine($"{canton.CantonPK}. {canton.Nombre}");
            }

            Console.Write("\n> Indique el cantón: ");
            string? cantonElegido = Console.ReadLine();

            if (int.TryParse(cantonElegido, out int cantonId))
            {
                var canton = context.Cantones
                    .Include(c => c.Distritos)
                    .Include(c => c.Provincia) // Necesario para obtener nombre de provincia
                    .FirstOrDefault(c => c.CantonPK == cantonId && c.ProvinciaFK == provincia.ProvinciaPK);

                if (canton != null)
                {
                    GenerarCSV.GenerarArchivoCSV(canton);
                }
                else
                {
                    Console.WriteLine("Cantón no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Entrada de cantón no válida.");
            }
        }
        else
        {
            Console.WriteLine("Provincia no encontrada.");
        }

    }
    else
    {
        Console.WriteLine("Entrada no válida.");
    }

}
