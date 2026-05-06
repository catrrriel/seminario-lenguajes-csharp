// Implementar un método para imprimir por consola todos los elementos de una matriz (arreglo de dos
// dimensiones) pasada como parámetro. Debe imprimir todos los elementos de una fila en la misma línea
// en la consola.

int [,] matriz = new int[3,4];

ImprimirMatriz(matriz);

void ImprimirMatriz(int[,] m)
{
    int ant=0;
    for (int i = 0; i < m.Length; i++)
    {
        int act=i/m.GetLength(1);
        m[i / m.GetLength(1), i % m.GetLength(1)] = i;
        
        if (ant == act)
        {
            Console.Write(m[i / m.GetLength(1), i % m.GetLength(1)]+"  ");
        }
        else
        {
            Console.WriteLine();
            Console.Write(m[i / m.GetLength(1), i % m.GetLength(1)]+"  ");
        }

        // Console.WriteLine($"ant:{ant} act:{act}");
        // Console.WriteLine($"{i/m.GetLength(1)}-{i%m.GetLength(1)}");
        ant=act;
    }
}

Console.ReadKey();