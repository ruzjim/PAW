# Caso Práctico 1

| Curso                         | Programación Avanzada en Web |
| :---------------------------- | :--------------------------- |
| Código                        | SC-701                       |
| Profesor                      | Luis Andrés Rojas Matey      |
| Fecha y hora de entrega final | Martes 24 de mayo a las 9 pm |
| Valor                         | 15 %                         |

<br />

## Especificaciones generales

El _solution_ contiene 3 _projects_ hechos con el _framework_ `.NET 8.0`, los cuales no tienen relación entre sí, es decir, son independientes:

- **Console**: una aplicación de consola.
- **MVC**: una aplicación web con arquitectura _Model-View-Controller_.
- **WebApi**: un servicio web del tipo _Minimal API_.

En cada proyecto se deben trabajar 5 categorías:

- **_Errors_**: arreglar los errores de compilación.
- **_Warnings_**: arreglar las advertencias de compilación.
- **_Fix_**: arreglar el error de ejecución.
- **_Update_**: incluir un nuevo componente o lógica de ejecución.
- **_Improvement_**: mejorar algún componente o lógica de ejecución.

Los **_Errors_** sin arreglar no permiten la ejecución del proyecto, por lo que esto es lo primero que se debe resolver.

Los **_Warnings_** sin arreglar permiten la ejecución del proyecto, pero también se deberían resolver; es decir, durante la compilación no se debería mostrar ningún error o advertencia. Se recomienda utilizar la opción `--no-incremental` durante la compilación para que siempre se muestren dichas advertencias:

```
$ dotnet build --no-incremental
```

<br />

## Console

Esta aplicación contiene una implementación del algoritmo de [Tribonacci](https://mathworld.wolfram.com/TribonacciNumber.html) de tipo recursivo (el método se llama `TribonacciRecursivo`). Esta secuencia está definida como:

```
T(1) = 1
T(2) = 1
T(3) = 2
T(n) = T(n - 1) + T(n - 2) + T(n - 3)
```

Al ejecutarse, el programa le consulta al usuario por un número entero positivo `n` (se puede suponer que el usuario siempre va a digitar un número entero) y luego ejecuta el algoritmo con ese número.

<br />

### **_Improvement_**

Mostrarle al usuario un mensaje cuando este digita un número que no está en el rango adecuado: mayor que 0 y menor que 40.

<br />

### **_Update_**

Implementar el algoritmo de Tribonacci de modo iterativo. Para esto se debe actualizar el método `TribonacciIterativo`.

<br />

### **_Fix_**

Arreglar la implementación de ambos algoritmos (`TribonacciRecursivo` y `TribonacciIterativo`) para que el resultado de `n = 38` y `n = 39` sean correctos (enteros positivos). Es decir, por ejemplo, este resultado es erróneo (`n = 39`):

```
Digite el valor de 'n': 39
Tribonacci Recursivo: -1543615208
Tribonacci Iterativo: -1543615208
```

Debido a que el correcto sería:

```
Digite el valor de 'n': 39
Tribonacci Recursivo: 7046319384
Tribonacci Iterativo: 7046319384
```

<br />

## MVC

Sitio web con arquitectura _Model-View-Controller_ con varias páginas.

<br />

### **_Fix_**

Arregle el _view_ **\_Layout.cshtml** para que permita cargar adecuadamente las páginas.

<br />

### **_Update_**

Agregue un _annotation_ al _action_ **Identificacion** (del _controller_ **CasaController**) para que solo sea accesible en la ruta `/Casa/Cedula/{id}`. Por ejemplo: `/Casa/Cedula/123456789`

<br />

### **_Improvement_**

Agregue, por medio de _annotations_, las siguientes validaciones a los _properties_ del _model_ **ClienteModel**:

- **Cedula**: `La cédula debe tener exactamente 9 dígitos.`
- **Edad**: `La edad debe estar entre 18 y 125.`
- **Nombre**: `El nombre no puede exceder los 10 caracteres.`
- **Correo**: `El correo electrónico debe contener el símbolo @.`

<br />

## WebApi

Este _web service_ contiene un par de _endpoints_:

- **/**: GET que redirige al UI de Swagger.
- **/log**: POST que retorna varias propiedades de una función logarítmica (`f(x) = logₐ(x)`), basado en el parámetro `a`, que representa su base.

<br />

### **_Fix_**

Arregle el _endpoint_ **"/log"** para que retorne un resultado válido (`HTTP 200 OK/Success`) cuando el valor del parámetro `a` cumple con que sea mayor que cero (0) y diferente de uno (1). Por ejemplo, cuando `a = 0.5`:

```xml
<?xml version="1.0" encoding="utf-16"?>
<Logaritmica xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <Funcion>f(x) = logₐ(x), a = 0.5</Funcion>
    <Monotonia>decreciente</Monotonia>
</Logaritmica>
```

<br />

### **_Improvement_**

Como parte del _response_ del _endpoint_ **"/log"**, incluya la definición de la función inversa de la función logarítmica (es decir, su función exponencial). Ejemplo, cuando `a = 0.5`:

```xml
<?xml version="1.0" encoding="utf-16"?>
<Logaritmica xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <Funcion>f(x) = logₐ(x), a = 0.5</Funcion>
    <Monotonia>decreciente</Monotonia>
    <Inversa>f⁻¹(x) = aˣ = 0.5ˣ</Inversa>
</Logaritmica>
```

<br />

### **_Update_**

Agregue un nuevo _endpoint_ **/saludo** con las siguientes características:

- Utiliza el método HTTP GET.

- Como parte de la ruta, recibirá un `nombre`, es decir **/saludo/{nombre}**, donde `nombre` es de tipo _string_.

- Si el `nombre` es diferente de nulo y tiene un largo mayor que cero, entonces retorna un resultado exitoso (`HTTP 200 OK/Success`) con un JSON que contenga un mensaje de saludo, de la siguiente forma: `"saludo": "¡Hola {nombre}!"`. Por ejemplo, el _request_ `/saludos/Luis` retornaría este _response_ exitoso (`HTTP 200 OK/Success`):

```json
{
  "saludo": "¡Hola Luis!"
}
```

- Si el `nombre` es nulo o su largo es menor que uno, entonces retorna un _response_ con un _status code_ `422` (`Unprocessable Entity`) y el _body_ vacío. Por ejemplo, con el _request_ `/saludos/` retornaría un _response_ de error `HTTP 422 Unprocessable Entity`.

<br />

## Rúbrica de evaluación

|                   | **Console** | **MVC** | **WebApi** |
| ----------------: | :---------: | :-----: | :--------: |
|      **_Errors_** |      1      |    1    |     1      |
|    **_Warnings_** |      1      |    1    |     1      |
|         **_Fix_** |      1      |    1    |     1      |
|      **_Update_** |      1      |    1    |     1      |
| **_Improvement_** |      1      |    1    |     1      |
|       **Totales** |    **5**    |  **5**  |   **5**    |

<br />

## Entregables

Un único archivo comprimido **ZIP** con el siguiente nombre: `CP1-[Carné].zip`. Ejemplo de nombre del archivo **ZIP**: `CP1-FH12345678.zip`.

Si el archivo comprimido no es del tipo correcto (**ZIP**) o el nombre del archivo no está en el formato correcto, entonces se evaluará como "no entregado".

El mismo (archivo comprimido **ZIP**) debe contener todo el código fuente que incluya el archivo _solution_ y las carpetas de los _projects_. Sin embargo, no debe contener los archivos compilados, es decir, excluir las carpetas `bin` y `obj`.

El código fuente debe incluir, en forma de comentarios, de dónde obtuvo la respuesta/implementación, ya sea el vínculo (_link_) de una página o el nombre del _chatbot_. Ejemplos:

```js
// https://mathworld.wolfram.com/TribonacciNumber.html

// Gemini
```
