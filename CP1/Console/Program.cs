
class Program
{
  static int TribonacciRecursivo(int n)
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
    else
    {
      // T(n) = T(n - 1) + T(n - 2) + T(n - 3)
      return TribonacciRecursivo(n - 1) + TribonacciRecursivo(n - 2) + TribonacciRecursivo(n - 3);
    }
  }

  static int TribonacciIterativo(int n)
  {
    return n;
  }

  static void Main(string args)
  {
    Console.Write("Digite el valor de 'n': ");
    var valor = Console.ReadLine();
    var n = int.Parse(valor);
    if (n > 0 && n < 40)
    {
      var recursivo = TribonacciRecursivo(n);
      var iterativo = TribonacciIterativo(n);
      Console.WriteLine($"Tribonacci Recursivo: {recursivo}");
      Console.WriteLine($"Tribonacci Iterativo: {iterativo}");
    }
  }
}