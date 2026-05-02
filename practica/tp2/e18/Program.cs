// Escribir una función (método int Fac(int n)) que calcule el factorial de un número n pasado al
// programa como parámetro por la línea de comando
// a) Definiendo una función no recursiva
// b) Definiendo una función recursiva
// c) idem a b) pero con expression-bodied methods (Tip: utilizar el operador condicional ternario)

Console.WriteLine(Fac(int.Parse(args[0])));
Console.WriteLine(FacRec(int.Parse(args[0])));
Console.WriteLine(FacExp(int.Parse(args[0])));

int Fac(int n)
{
    if(n==0)return 1;
    int aux=1;
    for (int i = 1; i <= n; i++)
    {
        aux*=i;
    }
    return aux;
}

int FacRec(int n)
{
    if (n == 0 || n == 1)
    {
        return 1;
    }
    return n*FacRec(n-1);
}

int FacExp (int n) => (n == 0 || n == 1) ? 1 : n * FacExp(n - 1);

Console.ReadKey();