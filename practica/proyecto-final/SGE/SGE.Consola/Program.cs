using Microsoft.EntityFrameworkCore;
using SGE.Infraestructura.Datos;

// Configuramos SQLite apuntando a un archivo local
var options = new DbContextOptionsBuilder<SgeContext>()
    .UseSqlite("Data Source=SGE.sqlite")
    .Options;

// Instanciamos contexto
using var context = new SgeContext(options);

// Llamamos al inicializador
SgeSqlite.Inicializar(context);

Console.WriteLine("Base de datos SGE.sqlite generada con exito");
Console.ReadKey();