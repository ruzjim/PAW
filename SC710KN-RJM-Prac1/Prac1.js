const { log } = require('console');
const readline = require('readline');

let phi;

console.clear();  //limpia todo
main();

async function main() {
  const n = await preguntar('Ingresa un valor entre 2 y 45: ');
  phi = calcularPhi();
  generarFibonacci(n);
  
} 
// Pregunta al usuario por un número entre 2 y 45
// y valida la entrada. Si no es un número válido, vuelve a preguntar
function preguntar(texto) {
  const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
  });
  
  return new Promise((resolve) => {
    
    preguntarNumero();
    
    function preguntarNumero() {
      rl.question(texto, (respuesta) => {
        const numero = parseInt(respuesta);
        if (isNaN(numero)) {
          console.log('El valor ingresado no es un número. Intenta de nuevo.');
          preguntarNumero();
        } else if (numero < 2 || numero > 45) {
          console.log('El número ingresado está fuera de rango. Intenta de nuevo.');
          preguntarNumero();
        } else {
          rl.close();
          resolve(numero);
        }
      });
    }
  });
}
// Calcula el número phi y lo muestra en consola
function calcularPhi() {
  // Fórmula para calcular el número áureo (phi)
  phi = (1 + Math.sqrt(5)) / 2;
  console.log();
  
  console.log(`Phi ~ ${phi.toFixed(10)}`);

  return phi;
}
// Genera la secuencia de Fibonacci hasta el n
// y muestra los cocientes de Fibonacci y su aproximación al número phi
function generarFibonacci(n) {
  console.log();
  let fib = [1, 1];
  for (let i = 2; i < n; i++) {
    fib[i] = fib[i - 1] + fib[i - 2];
  }
  for (let i = 1; i < n; i++) {
    const ratio = fib[i] / fib[i - 1];
    const diff = Math.abs(ratio - phi);
    console.log(`Fib(${i + 1}) / Fib(${i}) ~ ${ratio} [+- ${diff}]`);
  }
  console.log();
  console.log(`Fib(${n}) = ${fib[n - 1]}`);
  console.log(`Fib(${n - 1}) = ${fib[n - 2]}`);
  return fib;
}


















