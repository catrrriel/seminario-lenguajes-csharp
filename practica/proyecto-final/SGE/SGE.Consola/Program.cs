using Microsoft.EntityFrameworkCore;
using SGE.Infraestructura.Datos;

// 1. Configuramos SQLite apuntando a un archivo local
var options = new DbContextOptionsBuilder<SgeContext>()
    .UseSqlite("Data Source=SGE.sqlite")
    .Options;

// 2. Instanciamos tu nuevo contexto
using var context = new SgeContext(options);

// 3. Llamamos a tu inicializador
SgeSqlite.Inicializar(context);

Console.WriteLine("¡Base de datos SGE.sqlite generada con éxito!");
Console.ReadKey();