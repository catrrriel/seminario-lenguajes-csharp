// Idem. al ejercicio anterior salvo que se imprimirá un mensaje de saludo diferente según sea el
// nombre ingresado por el usuario. Así para “Juan” debe imprimir “¡Hola amigo!”, para “María” debe
// imprimir “Buen día señora”, para “Alberto” debe imprimir “Hola Alberto”. En otro caso, debe
// imprimir “Buen día ” seguido del nombre ingresado o “¡Buen día mundo!” si se ha ingresado una línea
// vacía.

// a) utilizando if ... else if
string? st;
Console.Write("Ingrese su nombre: ");
st = Console.ReadLine();
if(st != "")
{
    if(st == "Juan")
    {
        Console.WriteLine("Hola amigo!");
    }
    else if(st == "Maria")
    {
        Console.WriteLine("Buen dia señora");
    }
    else if(st == "Alberto")
    {
        Console.WriteLine("Hola Alberto");
    }
    else
    {
        Console.WriteLine("Buen dia "+st);
    }
}
else
{
    Console.WriteLine("Buen dia mundo!");
}

// b) utilizando switch
string? st2;
Console.Write("Ingrese su nombre: ");
st2 = Console.ReadLine();
switch (st2)
{
    case "":
        Console.WriteLine("Buen dia mundo!");
        break;
    case "Juan":
        Console.WriteLine("Hola amigo!");
        break;
    case "Maria":
        Console.WriteLine("Buen dia señora");
        break;
    case "Alberto":
        Console.WriteLine("Hola Alberto");
        break;
    default:
        Console.WriteLine("Buen dia "+st2);
        break;
}

Console.ReadKey(true);