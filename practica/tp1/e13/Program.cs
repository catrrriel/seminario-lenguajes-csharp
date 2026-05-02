// Si a y b son variables enteras, identificar el problema (y la forma de resolverlo) de la siguiente
// expresión. Tip: observar qué pasa cuando b = 0.
int a=10;
int b=0;

if ((b != 0) & (a/b > 5)) Console.WriteLine(a/b);

//El error esta en el operador logico, & evalua ambas condiciones. Por lo que intenta dividir por 0
//En cambio && evalua la primera y si no cumple ya no evalua la segunda.

Console.ReadKey();