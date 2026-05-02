// Escribir un programa que lea dos palabras separadas por un blanco que terminan con <ENTER>, y
// determinar si son simétricas (Ej: 'abbccd' y 'dccbba' son simétricas).
// Tip: si st es un string, entonces st[0] devuelve el primer carácter de st, y st[st.Length-1]
// devuelve el último carácter de st.

String? st1;
String? st2;
Boolean simetricas;

Console.WriteLine("Ingrese dos palabras separadas por un espacio:");
st1=Console.ReadLine();

if (st1 == null)
{
    Console.WriteLine("Entrada invalida");
    return;
}

int posEspacio=-1;
int i=0;

while(i<st1.Length && posEspacio==-1)
{
    if(st1[i]==' ')
    {
        posEspacio=i;
    }
    i++;
}

if (posEspacio == -1)
{
    Console.WriteLine("Entrada invalida");
    return;
}

int ini1=0;
int fin1=posEspacio-1;
int ini2=posEspacio+1;
int fin2=st1.Length-1;
simetricas=true;

if(fin1 - ini1 != fin2 - ini2)
{
    simetricas=false;
}
else
{
    int n=ini1;
    int j=fin2;

    while (n <= fin1 && simetricas)
    {
        if (st1[n] != st1[j])
        {
            simetricas=false;
        }

        n++;
        j--;
    }
}

Console.WriteLine("Las palabras son simetricas: "+simetricas);


// Console.WriteLine("Ingrese dos palabras: ");
// st1=Console.ReadLine();
// st2=Console.ReadLine();

// simetricas=true;

// if(st1.Length != st2.Length)
// {
//     simetricas=false;
// }
// else
// {
//     for (int i = 0; i < st1.Length; i++)
//     {
//         if(st1[i] != st2[st2.Length - 1 - i])
//         {
//             simetricas=false;
//             break;
//         }
//     }
// }

// if (simetricas)
// {
//     Console.WriteLine(st1+" y "+st2+" son palabras simetricas");
// }
// else
// {
//     Console.WriteLine(st1+" y "+st2+" no son palabras simetricas");
// }

Console.ReadKey();