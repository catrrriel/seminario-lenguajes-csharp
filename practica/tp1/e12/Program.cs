// Escribir un programa que imprima todos los divisores de un número entero ingresado desde la
// consola. Para obtener el entero desde un string st utilizar int.Parse(st).

String? st;

Console.WriteLine("Ingrese un numero entero: ");
st=Console.ReadLine();
int n=int.Parse(st);

for (int i = 1; i <= n; i++)
{
    if (n % i == 0)
    {
        Console.WriteLine(i+" <= divisor de "+n);
    }
}

Console.ReadLine();