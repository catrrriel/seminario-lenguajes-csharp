// Implementar los métodos GetDiagonalPrincipal y GetDiagonalSecundaria que devuelven
// un vector con la diagonal correspondiente de la matriz pasada como parámetro. Si la matriz no es
// cuadrada generar una excepción ArgumentException.

double[,] m = new double [,]{{1,2,3},{4,5,6},{7,8,9}};

try
{
    double[]? vPrincipal=GetDiagonalPrincipal(m);
    if (vPrincipal!=null) ImprimirVector(vPrincipal);
    
    Console.WriteLine();
    
    double[]? vSecundaria=GetDiagonalSecundaria(m);
    if (vSecundaria!=null) ImprimirVector(vSecundaria);
}
catch (ArgumentException e)
{
    Console.WriteLine(e.Message);
    Console.WriteLine("'La matriz no es simetrica.'");
}

void ImprimirVector(double[] v)
{
    foreach (double n in v)
    {
        Console.Write($"[{n.ToString()}]");
    }
}

double[]? GetDiagonalPrincipal(double[,] matriz)
{
    if (matriz.GetLength(0) != matriz.GetLength(1)) throw new ArgumentException();

    double[] vec=new double[matriz.GetLength(1)];
    for (int i = 0; i < matriz.GetLength(0); i++)
    {
        vec[i]=matriz[i,i];
    }
    return vec;
}

double[]? GetDiagonalSecundaria(double[,] matriz)
{
    if (matriz.GetLength(0) != matriz.GetLength(1)) throw new ArgumentException();
    
    int dim = matriz.GetLength(0);
    double[] vec = new double[dim];
    for (int i = 0; i < dim; i++)
    {
       vec[i]=matriz[i,dim - 1 - i];
    }
    return vec; 
}

Console.ReadKey();