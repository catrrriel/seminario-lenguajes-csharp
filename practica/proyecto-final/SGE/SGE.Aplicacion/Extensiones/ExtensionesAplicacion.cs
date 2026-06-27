using Microsoft.Extensions.DependencyInjection;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;
using SGE.Aplicacion.Usuarios;
namespace SGE.Aplicacion.Extensiones;
public static class ExtensionesAplicacion
{
    public static IServiceCollection AddAplicacion(this IServiceCollection servicios)
    {
        // Casos de uso de Usuario
        servicios.AddScoped<LoginUseCase>();
        servicios.AddScoped<RegistrarUsuarioUseCase>();
        servicios.AddScoped<EliminarUsuarioUseCase>();
        servicios.AddScoped<ModificarMisDatosUseCase>();
        servicios.AddScoped<ModificarPermisosUsuarioUseCase>();
        servicios.AddScoped<ListarUsuariosUseCase>();
        
        // Expediente
        servicios.AddScoped<AgregarExpedienteUseCase>();
        servicios.AddScoped<EliminarExpedienteUseCase>();
        servicios.AddScoped<CambiarEstadoExpedienteUseCase>();
        servicios.AddScoped<ListarExpedientesUseCase>();
        servicios.AddScoped<ObtenerDetalleExpedienteUseCase>();
        servicios.AddScoped<ModificarCaratulaExpedienteUseCase>();
        
        // Tramite
        servicios.AddScoped<AgregarTramiteUseCase>();
        servicios.AddScoped<EliminarTramiteUseCase>();
        servicios.AddScoped<ListarTramitesPorExpedienteUseCase>();
        servicios.AddScoped<ModificarTramiteUseCase>();
        servicios.AddScoped<ActualizacionEstadoExpedienteService>();

        return servicios;
    }
}