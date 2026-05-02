// Qué líneas del siguiente código provocan conversiones boxing y unboxing.

char c1 = 'A';
string st1 = "A";
object o1 = c1;     // <= boxing
object o2 = st1;
char c2 = (char)o1; // <= unboxing
string st2 = (string)o2;

// ¿Qué diferencias existen entre las conversiones de tipo implícitas y explícitas?

// La diferencia esta en el consentimiento del programador y si puede haber perdida de datos

// Conversion Implicita
//  - La hace el compilador automaticamente
//  - No requiere cast
//  - Es segura, no hay perdida de datos

// Conversion Explicita
//  - Requiere que la haga el programador
//  - Requiere cast
//  - Puede haber perdida de datos. por ej de double a int se pierden los decimales, 
//    por eso estas obligado a escribir (int)

Console.ReadKey();