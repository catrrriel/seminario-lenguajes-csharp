// Utilizar la clase Stack<T> (pila) para implementar un programa
// que pase un número en base 10 a otra base realizando divisiones
// sucesivas. Por ejemplo para pasar 35 en base 10 a binario dividimos
// sucesivamente por dos hasta encontrar un cociente menor que el
// divisor, luego el resultado se obtiene leyendo de abajo hacia arriba el
// cociente de la última división seguida por todos los restos.

Stack<int> pila = new Stack<int>();
int n = 35;
int baseN = 2;
Console.WriteLine($"numero en base 10 a convertir: {n}");

while(n > 0)
{
    Console.WriteLine(n % baseN);
    pila.Push(n % baseN);
    n /= baseN;
}

while(pila.Count > 0)
{
    int num = pila.Pop();

    if(num < 10)
    {
        Console.Write(num);
    }
    else
    {
        Console.Write((char)('A' + num - 10));
    }
}
Console.WriteLine($" <- base {baseN}");

Console.ReadKey();