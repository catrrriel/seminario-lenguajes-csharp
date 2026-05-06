// Realizar un análisis sintáctico para determinar si los paréntesis en una expresión aritmética están
// bien balanceados. Verificar que por cada paréntesis de apertura exista uno de cierre en la cadena de
// entrada. Utilizar una pila para resolverlo. Los paréntesis de apertura de la entrada se almacenan en una
// pila hasta encontrar uno de cierre, realizándose entonces la extracción del último paréntesis de apertura
// almacenado. Si durante el proceso se lee un paréntesis de cierre y la pila está vacía, entonces la cadena
// es incorrecta. Al finalizar el análisis, la pila debe quedar vacía para que la cadena leída sea aceptada, de
// lo contrario la misma no es válida.

Stack<char> pila = new Stack<char>();
string expresion="((1+2)+(2*(3+4))*3)";
char c;
bool esValida=true;
int i=0;

while(i < expresion.Length && esValida)
{
    c=expresion[i];
    switch (c)
    {
        case ')':
            if(pila.Count == 0)
            {
                esValida = false;
            }
            else
            {
                pila.Pop();
            }
            break;
        case '(':
            pila.Push(c);
            break;
    }
    i++;
}

if (pila.Count == 0 && esValida)
{
    Console.WriteLine($"La expresion {expresion} es valida");
}
else
{
    Console.WriteLine($"La expresion {expresion} no es valida");
}

Console.ReadKey();