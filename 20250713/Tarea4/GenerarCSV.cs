public static class GenerarCSV
{
    public static void GenerarArchivoCSV(Canton canton)
    {
        string folder = Path.Combine(Environment.CurrentDirectory, "data");
        Directory.CreateDirectory(folder);

        // Usamos las PKs correctamente desde las entidades
        string nombreArchivo = $"{canton.Provincia.ProvinciaPK}-{canton.CantonPK}.csv";
        Console.Write($"\nGenerando y guardando el archivo {nombreArchivo}...");
        string rutaArchivo = Path.Combine(folder, nombreArchivo);

        using var writer = new StreamWriter(rutaArchivo);

        // Escribir encabezado
        writer.WriteLine("Provincia,Cantón,Distrito");

        // Escribir cada distrito
        foreach (var distrito in canton.Distritos.OrderBy(d => d.Nombre))
        {
            writer.WriteLine($"{canton.Provincia.Nombre},{canton.Nombre},{distrito.Nombre}");
        }
        Console.WriteLine(" Listo.");
    }
}
