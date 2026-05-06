// Implementar un método que devuelva un arreglo de arreglos con los mismos elementos que la matriz
// pasada como parámetro:
double[,] m = new double[,]{{1,2,3},{4,5,6},{7,8,9}};
double[][] arr = GetArregloDeArreglos(m);
ImprimirArregloDeArreglos(arr);

double[][] GetArregloDeArreglos(double[,] m)
{
    int dimF=m.GetLength(0);
    int dimC=m.GetLength(1);

    double[][] v = new double[dimF][];
    for (int i = 0; i < dimF; i++)
    {
        v[i] = new double[dimC];
        for (int j = 0; j < dimC; j++)
        {
            v[i][j] = m[i,j];
        }
    }
    return v;
}

void ImprimirArregloDeArreglos(double[][] a)
{
    foreach (var fila in a) // <= inferencia de tipos, var ya sabe de que tipo es
    {
        foreach (var elem in fila)
        {
            Console.Write($"[{elem}]");
        }
        Console.WriteLine();
    }
}

Console.ReadKey();
