// ¿Para qué sirve el método Split de la clase string? Usarlo para escribir en la consola todas las
// palabras (una por línea) de una frase ingresada por consola por el usuario.

string phrase="Armani, el taco, no... hace la personal, y ahi se va... se va..."+
              " se viene Martinez para el gol... y va el tercero, y va el tercero, y va el tercero,"+
              "y gol de River, gol de River... gooooooooool";


//ej 1
string[] words=phrase.Split(" ");
foreach (string s in words)
{
    Console.WriteLine(s);
}
Console.WriteLine();

//ej 2
char[] delimiters={',', '.'};
string[] words2=phrase.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
foreach (string s in words2)
{
    Console.WriteLine(s);
}

Console.ReadKey();