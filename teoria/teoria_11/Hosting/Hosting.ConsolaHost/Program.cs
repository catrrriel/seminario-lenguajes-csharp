using Hosting.Aplicacion;
using Microsoft.Extensions.DependencyInjection;

var servicios = new ServiceCollection();

// Registramos los servicios
servicios.AddTransient<ILogger, LoggerConsola>();
servicios.AddTransient<IServicioX, ServicioX>();

// Construimos el proveedor
using var proveedor = servicios.BuildServiceProvider();

// Solicitamos los servicios al contenedor
var servicioX = proveedor.GetService<IServicioX>();
servicioX?.Ejecutar();

var logger = proveedor.GetService<ILogger>();
logger?.Log("Fin del programa con DI Container oficial");
Console.ReadKey();