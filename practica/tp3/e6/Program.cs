// Implementar los siguientes métodos que devuelvan la suma, resta y multiplicación de matrices
// pasadas como parámetros. Para el caso de la suma y la resta, las matrices deben ser del mismo tamaño,
// en caso de no serlo devolver null. Para el caso de la multiplicación la cantidad de columnas de A debe
// ser igual a la cantidad de filas de B, en caso contrario generar una excepción ArgumentException.

double[,] m1 = new double[,]{{1,2},{3,4}};
double[,] m2 = new double[,]{{5,6},{7,8}};
double[,]? res;

res=SumaMatrices(m1,m2);
if(res != null)
{
    Console.WriteLine("----- Suma -----");
    ImprimirMatrizConFormato(res,"0.0");
}

res=RestaMatrices(m1,m2);
if(res != null)
{
    Console.WriteLine("----- Resta -----");
    ImprimirMatrizConFormato(res,"0.0");
}

try
{
    res = MultMatrices(m1,m2);
    Console.WriteLine("----- Multiplicacion -----");
    ImprimirMatrizConFormato(res,"0.0");
}
catch (ArgumentException e)
{
    Console.WriteLine(e);
}

double[,]? SumaMatrices(double[,] A, double[,] B)
{
    int dimF1 = A.GetLength(0);
    int dimF2 = B.GetLength(0);
    int dimC1 = A.GetLength(1);
    int dimC2 = B.GetLength(1);
    if(dimF1 == dimF2 && dimC1 == dimC2)
    {
        double[,] res = new double [dimF1,dimC1];
        for (int i = 0; i < dimF1; i++)
        {
            for (int j = 0; j < dimC1; j++)
            {
                res[i,j] = A[i,j] + B[i,j];
            }
        }
        return res;
    }
    else
    {
        Console.WriteLine("las matrices no tienen las mismas dimensiones para ejecutar la suma");
        return null;
    }
}

double[,]? RestaMatrices(double[,] A, double[,] B)
{
    int dimF1 = A.GetLength(0);
    int dimF2 = B.GetLength(0);
    int dimC1 = A.GetLength(1);
    int dimC2 = B.GetLength(1);
    if(dimF1 == dimF2 && dimC1 == dimC2)
    {
        double[,] res = new double [dimF1,dimC1];
        for (int i = 0; i < dimF1; i++)
        {
            for (int j = 0; j < dimC1; j++)
            {
                res[i,j] = A[i,j] - B[i,j];
            }
        }
        return res;
    }
    else
    {
        Console.WriteLine("las matrices no tienen las mismas dimensiones para ejecutar la resta");
        return null;
    }
}

double[,] MultMatrices(double[,] A, double[,] B)
{
    int filasA = A.GetLength(0);
    int filasB = B.GetLength(0);
    int columA = A.GetLength(1);
    int columB = B.GetLength(1);

    if(columA != filasB)
        throw new ArgumentException("no se pueden multiplicar las matrices");
    
    double[,] res = new double[filasA,columB];

    for (int i = 0; i < filasA; i++)
    {
        for (int j = 0; j < columB; j++)
        {
            double suma = 0;
            for (int k = 0; k < columA; k++)
            {
                suma += A[i,k]*B[k,j];
            }
            res[i,j] = suma;
        }
    }

    return res;
}

void ImprimirMatrizConFormato(double[,] m, string f)
{
    int ant=0;
    for (int i = 0; i < m.Length; i++)
    {
        int act=i/m.GetLength(1);
        if (ant == act)
        {
            Console.Write($"[{m[i / m.GetLength(1), i % m.GetLength(1)].ToString(f),5}]");
        }
        else
        {
            Console.WriteLine();
            Console.Write($"[{m[i / m.GetLength(1), i % m.GetLength(1)].ToString(f),5}]");
        }
        ant=act;
    }
    Console.WriteLine();
}

Console.ReadKey();