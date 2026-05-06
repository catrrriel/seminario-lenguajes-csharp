// Implementar el método ImprimirMatrizConFormato, similar al anterior pero ahora con un
// parámetro más que representa la plantilla de formato que debe aplicarse a los números al imprimirse.
// La plantilla de formato es un string de acuerdo a las convenciones de formato compuesto, por ejemplo
// “0.0” implica escribir los valores reales con un dígito para la parte decimal.
double [,] matriz = new double[3,4];
string formato="0.000";
ImprimirMatrizConFormato(matriz,formato);

void ImprimirMatrizConFormato(double[,] m, string f)
{
    int ant=0;
    for (int i = 0; i < m.Length; i++)
    {
        int act=i/m.GetLength(1);
        m[i / m.GetLength(1), i % m.GetLength(1)] = i*1.912;
        
        if (ant == act)
        {
            Console.Write($"{m[i / m.GetLength(1), i % m.GetLength(1)].ToString(f),9}");
        }
        else
        {
            Console.WriteLine();
            Console.Write($"{m[i / m.GetLength(1), i % m.GetLength(1)].ToString(f),9}");
        }

        // Console.WriteLine($"ant:{ant} act:{act}");
        // Console.WriteLine($"{i/m.GetLength(1)}-{i%m.GetLength(1)}");
        ant=act;
    }
}

Console.ReadKey();