using var normalDb = new NormalContext();

Console.WriteLine($"Normal: {normalDb.Database.ProviderName}");