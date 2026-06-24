using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SGE.Aplicacion.Expedientes;
using SGE.Infraestructura.Expedientes;
using SGE.Infraestructura.Tramites;
using SGE.Infraestructura.Usuarios;
using SGE.Infraestructura.UnidadesDeTrabajo;
using SGE.Aplicacion.Tramites;
using SGE.Aplicacion.Usuarios;
using SGE.Aplicacion.Abstracciones;
using SGE.Infraestructura.Autorizacion;
using SGE.Aplicacion.Autorizacion;
using SGE.Infraestructura.Datos;
using Microsoft.EntityFrameworkCore;

namespace SGE.Infraestructura.Extensiones;
public static class Extensiones
{
    public static IServiceCollection AddInfraestructura(this IServiceCollection servicios, IConfiguration configuration)
    {
        // Extraemos la cadena de conexion a la base de datos leyendo appsettings.json
        var connectionString = configuration.GetConnectionString("SgeDb");
        // Registramos el SgeContext como scoped
        servicios.AddDbContext<SgeContext>(opciones => opciones.UseSqlite(connectionString));

        // El ciclo de vida Scoped aca garantiza que la unidad de trabajo y los repositorios
        // compartan la misma transaccion en memoria durante cada peticion HTTP
        servicios.AddScoped<IExpedienteRepository, ExpedienteRepository>();
        servicios.AddScoped<ITramiteRepository, TramiteRepository>();
        servicios.AddScoped<IUsuarioRepository, UsuarioRepository>();
        servicios.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
        
        // Seguridad
        // Como el servicio de autorizacion usa DbContext, debe ser Scoped. (Dependencia con SgeContext)
        servicios.AddScoped<IAutorizacionService, AutorizacionService>();
        servicios.AddScoped<IHashService, HashService>();

        return servicios;
    }
}