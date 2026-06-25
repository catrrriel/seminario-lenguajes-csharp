using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Extensiones;
using SGE.Dominio.Comun;
using SGE.Infraestructura.Datos;
using SGE.Infraestructura.Extensiones;
using SGE.WebApi.Endpoints;
using SGE.WebApi.ManejadorDeExcepciones;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURACION DE SERVICIOS (Contenedor DI)
// Open Api
builder.Services.AddOpenApi();
// DB, Repositorios, UOW, servicios de seguridad
builder.Services.AddInfraestructura(builder.Configuration);
// Casos de uso
builder.Services.AddAplication();
// Soporte para el formato estandar de errores
builder.Services.AddProblemDetails();
// Registramos nuestro manejador
builder.Services.AddExceptionHandler<ManejadorDeExcepcionesGlobales>();

// Constuimos la aplicacion y cerramos la fase de configuracion
var app = builder.Build();

// Solo exponemos esto en modo desarrollo
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Genera el archivo JSON interno
    app.MapScalarApiReference(); // Levanta la interfaz grafica en /scalar
}

// Middleware al principio del pipeline
app.UseExceptionHandler();

// INICIALIZACION DE BASE DE DATOS
// Como SgeContext se registro como Scoped, su ciclo de vida natural
// exige que exista dentro de una peticion HTTP. Como el servidor recien arranca 
// y aun no hay peticiones, "simulamos" una creando un Scope temporal.
// Asi podemos pedirle el contexto al contenedor de forma segura.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SgeContext>();
    var hashService = scope.ServiceProvider.GetRequiredService<IHashService>();

    SgeSqlite.Inicializar(context, hashService);
}

// ENDPOINTS
app.MapGet("/", () => "La API de Sge esta funcionando");

app.MapExpedientesEndpoints();
app.MapTramitesEndpoints();

app.MapGet("/api/prueba-error", () => 
{
    throw new DominioException("El manejador atrapo este error de prueba");
});

app.Run();
