var x = new { Nombre = "Juan", Edad = 28 };
var y = new { Alto = 12.4, Ancho = 11, Largo = 20 };
Console.WriteLine(x.GetType());
Console.WriteLine(new { Ciudad = "La Plata", CP = 1900 }.GetType());
Console.WriteLine(new { Nombre = "Ana", Edad = 2 }.GetType());
Console.WriteLine(y.GetType());
Console.WriteLine(new { Ancho = 11, Alto = 12.4, Largo = 20 }.GetType());


Console.ReadKey();