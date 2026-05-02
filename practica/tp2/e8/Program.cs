// Investigar acerca de la clase StringBuilder del espacio de nombre System.Text ¿En qué
// circunstancias es preferible utilizar StringBuilder en lugar de utilizar string? Implementar un
// caso de ejemplo en el que el rendimiento sea claramente superior utilizando StringBuilder en lugar
// de string y otro en el que no.
using System.Text;

// inicializar
StringBuilder sb = new StringBuilder("Club Atletico");
Console.WriteLine(sb);

// appendLine => agregar al final y salto de linea
sb.AppendLine(" River Plate");

// append => manera estandar de agregar al final 
sb.Append("El mas grande, lejos");
Console.WriteLine(sb);

// insert => insertar el string en la pos indicada
sb.Insert(18,"912");

// replace => reemplazar el primer string por el segundo
sb.Replace("912r","255");
Console.WriteLine(sb);

// toString => convertir a standard string
string st = sb.ToString();
Console.WriteLine(st);
Console.WriteLine();

// ejemplos
StringBuilder strb = new StringBuilder("");
for (int i = 1; i <= 100000; i++)
{
    strb.Append(i);
}
string resultado=strb.ToString();
Console.WriteLine(resultado.Length);
DateTime now2= DateTime.Now;
Console.WriteLine(now2.ToString());

string str="";
for (int i = 1; i <= 50000; i++)
{
    str+=i;
}
Console.WriteLine(str.Length);
DateTime now1= DateTime.Now;
Console.WriteLine(now1.ToString());

Console.ReadKey();