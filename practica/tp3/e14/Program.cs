// Utilizar la clase Queue<T> para implementar un programa que realice el cifrado de un texto
// aplicando la técnica de clave repetitiva. La técnica de clave repetitiva consiste en desplazar un carácter
// una cantidad constante de acuerdo a la lista de valores que se encuentra en la clave. Por ejemplo: para
// la siguiente tabla de caracteres:

// A B C D E F G H I J  K  L  M  N  Ñ  O  P  Q  R  S  T  U  V  W  X  Y  Z  sp
// 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28

// A cada carácter del mensaje original se le suma la cantidad indicada en la clave. En el caso que la suma
// fuese mayor que 28 se debe volver a contar desde el principio, Implementar una cola con los números
// de la clave encolados y a medida que se desencolen para utilizarlos en el cifrado, se vuelvan a encolar
// para su posterior utilización. Programar un método para cifrar y otro para descifrar.

Queue<int> claveOriginal = new Queue<int>();
claveOriginal.Enqueue(5);
claveOriginal.Enqueue(3);
claveOriginal.Enqueue(9);
claveOriginal.Enqueue(7);

Console.WriteLine(Cifrar("CLUB ATLETICO RIVER PLATE", claveOriginal));
Console.WriteLine(Descifrar("HÑCIEDBRJWQJTC O H GUÑJ J", claveOriginal));

string Cifrar(string st, Queue<int> claveOriginal)
{
    Queue<int> clave = new Queue<int>(claveOriginal);
    string alfabeto = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ ";
    string res = "";

    foreach (char c in st)
    {
        int posLetra = alfabeto.IndexOf(c) + 1;
        int k = clave.Dequeue();
        clave.Enqueue(k);

        int nueLetra = posLetra + k;
        if(nueLetra>28)
            nueLetra-=28;
        
        res+=alfabeto[nueLetra - 1];
    }

    return res;
}

string Descifrar(string st, Queue<int> claveOriginal)
{
    Queue<int> clave = new Queue<int>(claveOriginal);
    string alfabeto = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ ";
    string res = "";

    foreach (char c in st)
    {
        int posLetra = alfabeto.IndexOf(c) + 1;
        int k = clave.Dequeue();
        clave.Enqueue(k);

        int nueLetra = posLetra - k;
        if(nueLetra<=0)
            nueLetra+=28;
        
        res+=alfabeto[nueLetra - 1];
    }

    return res;
}

Console.ReadKey();