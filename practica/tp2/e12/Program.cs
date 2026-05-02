// Definir el tipo de datos enumerativo llamado Meses y utilizarlo para:
// a) Imprimir en la consola el nombre de cada uno de los meses en orden inverso (diciembre,
// noviembre, octubre …, enero)
// b) Solicitar al usuario que ingrese un texto y responder si el texto tipeado corresponde al nombre de
// un mes
// Nota: en todos los casos utilizar un for iterando sobre una variable de tipo Meses


for (Meses m = Meses.Diciembre; m >= Meses.Enero ; m--)
{
    Console.WriteLine(m);
}
Console.ReadKey();


enum Meses
{
    Enero,Febrero,Marzo,Abril,Mayo,Junio,Julio,
    Agosto,Septiembre,Octubre,Noviembre,Diciembre
}


