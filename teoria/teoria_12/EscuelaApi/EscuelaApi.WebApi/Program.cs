using EscuelaApi.Aplicacion;
using EscuelaApi.Infraestructura;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ====================================================================
// BLOQUE 1: CONFIGURACIÓN DE SERVICIOS (El Contenedor DI)
// ====================================================================

// A. Base de Datos
// Extraemos la cadena de conexión del archivo appsettings.json
var connectionString = builder.Configuration.GetConnectionString("EscuelaDb");
// El siguiente método registra EscuelaContext como scoped
builder.Services.AddDbContext<EscuelaContext>(opciones => 
    opciones.UseSqlite(connectionString));

// B. Patrón Unit of Work y Repositorios
// ¡ATENCIÓN! El ciclo de vida 'Scoped' es vital aquí.
// Garantiza que la Unidad de Trabajo y los Repositorios compartan 
// exactamente la misma transacción en memoria durante cada petición HTTP.
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
builder.Services.AddScoped<IExamenRepository, ExamenRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// C. Seguridad (Autorización)
// El servicio de autorización, como usa el DbContext, debe ser Scoped 
// (tiene una dependencia con EscuelaContext)
builder.Services.AddScoped<IAutorizacionService, AutorizacionService>();


// D. Casos de Uso
builder.Services.AddScoped<AgregarAlumnoUseCase>();
builder.Services.AddScoped<RegistrarExamenUseCase>();
builder.Services.AddScoped<ConsultarExamenesAlumnoUseCase>();
builder.Services.AddScoped<ModificarNotaDeExamenUseCase>();
builder.Services.AddScoped<EliminarAlumnoUseCase>();
builder.Services.AddScoped<ConsultarAlumnosUseCase>();
builder.Services.AddScoped<ConsultarUsuariosUseCase>();

// Construimos la aplicación y cerramos la fase de configuración
var app = builder.Build();

// ====================================================================
// BLOQUE 2: INICIALIZACIÓN DE LA BASE DE DATOS 
// ====================================================================
// Como EscuelaContext se registró como "Scoped", su ciclo de vida natural 
// exige que exista dentro de una petición HTTP. Como el servidor recién 
// arranca y aún no hay peticiones, "simulamos" una creando un Scope 
// temporal. Así podemos pedirle el contexto al contenedor de forma segura.

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EscuelaContext>();
    EscuelaSqlite.Inicializar(context);
}


// ====================================================================
// BLOQUE 3: ENDPOINT DE PRUEBA (Sanity Check)
// ====================================================================

app.MapGet("/", () => "¡La API de la Escuela está funcionando correctamente!");

// ====================================================================
// Aquí comenzaremos a escribir nuestros endpoints complejos en la clase...
// ====================================================================


app.Run();
