// ¿Qué líneas del siguiente método provocan error de compilación y por qué?

var a = 3L;     // <= long 64bits
dynamic b = 32;
object obj = 3;
a = a * 2.0;    // <= mal, no se puede convertir implicitamente de double a long. solucion: (long)(a * 2.0)
b = b * 2.0;
b = "hola";
obj = b;        // <= obj = hola
b = b + 11;     // <= b = hola11
obj = obj + 11; // <= mal, no se puede usar el operador + entre object e int
var c = new { Nombre = "Juan" };
var d = new { Nombre = "María" };
var e = new { Nombre = "Maria", Edad = 20 };
var f = new { Edad = 20, Nombre = "Maria" };
f.Edad = 22;    // <= mal, los campos de los tipos anonimos son de solo lectura
d = c;          // <= esta bien porque lo que cambia es la referencia de la variable d
e = d;          // <= mal, son de distinto tipo
f = e;          // <= mal, son de distinto tipo, el orden de los campos en los tipos anonimos importa

Console.ReadKey();