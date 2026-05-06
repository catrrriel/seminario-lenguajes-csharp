int i = 10;
var x = i * 1.0;
var y = 1f;
var z = i * y;

Console.WriteLine(x.GetType()); // <= double
Console.WriteLine(y.GetType()); // <= float/single
Console.WriteLine(z.GetType()); // <= float/single

Console.ReadKey();