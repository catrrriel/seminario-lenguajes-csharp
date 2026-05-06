// ¿Qué se puede inferir respecto de la excepción división por cero en relación al tipo de los operandos?

int x = 0;
try
{
    Console.WriteLine(1.0 / x);     // <= infinito
    Console.WriteLine(1 / x);       // <= excepcion
    Console.WriteLine("todo OK");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

// Enteros (int, long, decimal): La division por cero es una operacion invalida que interrumpe la ejecucion
// del programa con un error. Los enteros truncan la parte fraccionaria al dividir, lo que no permite la
// aproximacion al infinito

// Punto Flotante (double, float): Diseñados para calculos cientificos, estos tipos manejan la division 
// por cero devolviendo valores especiales (double.PositiveInfinity, double.NegativeInfinity o double.NaN) 
// en lugar de lanzar una excepcion.

Console.ReadKey();