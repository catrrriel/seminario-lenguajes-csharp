object obj = new int[10];
dynamic dyna = 13;

Console.WriteLine(obj.Length); // <= object no contiene una definicion para length, error de compilacion

Console.WriteLine(dyna.Length); // <= en este caso dynamic esta asignado con int que tampoco 
                                //    tiene una definicion para length, error de ejecucion

Console.ReadKey();