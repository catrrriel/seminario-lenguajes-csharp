// 15 ¿Cuál es el problema del código siguiente y cómo se soluciona?

// int i = 0;
// for (i = 1; i <= 10;)
// {
// Console.WriteLine(i++);
// }

// 16 Analizar el siguiente código. ¿Cuál es la salida por consola?

int i = 1;
if (--i == 0) //aca resta primero por lo que la evaluacion da true (0)
{
Console.WriteLine("cero"); //imprime
}
if (i++ == 0) //aca primero evalua y despues suma, por lo que tambien da true (sigue siendo 0)
{
Console.WriteLine("ceroo"); // imprime
}
Console.WriteLine(i); // pero aca ya esta incrementado en 1
Console.ReadKey();