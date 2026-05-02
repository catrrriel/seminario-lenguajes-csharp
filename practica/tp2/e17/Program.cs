// Implementar un programa que muestre todos los números primos entre 1 y un número natural dado
// (pasado al programa como argumento por la línea de comandos). Definir el método bool
// EsPrimo(int n) que devuelve true sólo si n es primo. Esta función debe comprobar si n es
// divisible por algún número entero entre 2 y la raíz cuadrada de n. (Nota: Math.Sqrt(d) devuelve la
// raíz cuadrada de d)

for (int i = 1; i < int.Parse(args[0]); i++)
{
    if (EsPrimo(i))
    {
        Console.WriteLine(i);
    }
}

bool EsPrimo(int n)
{
    if(n<2) return false;
    int i=2;
    while (i <= Math.Sqrt(n))
    {
        if(n % i == 0)
        {
            return false;
        }
        i++;
    }
    return true;
}

Console.ReadKey();