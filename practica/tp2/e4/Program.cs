//Resolver los errores de compilación en el siguiente fragmento de código. Utilizar el operador as
//cuando sea posible.

object o = "Hola Mundo!";
string? st = o as String;
int i = 12;
byte b = (byte)i;
double d = i;
float f = (float)d;
o = i;
i = (int)o + 1;