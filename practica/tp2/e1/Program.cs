object o1 = "A";
object o2 = o1;
o2 = "Z";
Console.WriteLine(o1 + " " + o2);
Console.ReadKey();

// El tipo object es un tipo referencia, por lo tanto luego de la sentencia o2 = o1 ambas variables están
// apuntando a la misma dirección. ¿Cómo explica entonces que el resultado en la consola no sea “Z Z”?

// Porque cuando se asigna un nuevo valor a o2, este busca una nueva direccion en heap para almacenarla.
// Por ende son distintas variables distintas direcciones a la hora de hacer writeline.