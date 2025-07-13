
class Program
{
  static Int64 TribonacciRecursivo(int n)
  {
    // return n;
    if (n == 1 || n == 2)
    {
      // T(1) = 1
      // T(2) = 1
      return 1;
    }
    else if (n == 3)
    {
      // T(3) = 2
      return 2;
    }
    else
    {
      // T(n) = T(n - 1) + T(n - 2) + T(n - 3)
      return TribonacciRecursivo(n - 1) + TribonacciRecursivo(n - 2) + TribonacciRecursivo(n - 3);
    }
  }
  
//autogenerado con copilot el int lo subi a Int64 pensando el problema
  static Int64 TribonacciIterativo(int n)
  {
    if (n == 1 || n == 2)
    {
      // T(1) = 1
      // T(2) = 1
      return 1;
    }
    else if (n == 3)
    {
      // T(3) = 2
      return 2;
    }

    Int64 t1 = 1, t2 = 1, t3 = 2, tn = 0;

    for (int i = 4; i <= n; i++)
    {
      tn = t1 + t2 + t3; // T(n) = T(n - 1) + T(n - 2) + T(n - 3)
      t1 = t2; // Actualizar T(n-3)
      t2 = t3; // Actualizar T(n-2)
      t3 = tn; // Actualizar T(n-1)
    }

    return tn;
  }

  //https://chatgpt.com/share/685b42c6-03fc-8003-a7c6-4f462068a887
  static void Main(string[] args)
  {
    Console.Write("Digite el valor de 'n': ");
    var valor = Console.ReadLine();
    if (!int.TryParse(valor, out int n))
    {
      Console.WriteLine("Entrada inválida. Debe ser un número entero.");
      return;
    }

    if (n > 0 && n < 40)
    {
      var recursivo = TribonacciRecursivo(n);
      var iterativo = TribonacciIterativo(n);
      Console.WriteLine($"Tribonacci Recursivo: {recursivo}");
      Console.WriteLine($"Tribonacci Iterativo: {iterativo}");
    }
    else //autogenerado con copilot
    {
      Console.WriteLine("El número debe ser mayor que 0 y menor que 40.");
    }
  }
}