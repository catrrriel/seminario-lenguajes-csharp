// Escribir un programa que solicite al usuario ingresar su nombre e imprima en la consola un saludo
// personalizado utilizando ese nombre o la frase “Hola mundo” si el usuario ingresó una línea en blanco.
// Para ingresar un string desde el teclado utilizar Console.ReadLine()

String? st;

Console.Write("Ingrese su nombre: ");
st = Console.ReadLine();

if (st != "")
{
    Console.WriteLine("Hola "+st);
}
else
{
    Console.WriteLine("Hola mundo");
}


Console.ReadKey();