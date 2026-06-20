using EscuelaApi.Aplicacion;
using EscuelaApi.Dominio;

namespace EscuelaApi.Infraestructura;

public class AutorizacionService(EscuelaContext context) : IAutorizacionService
{
    public bool TieneRol(Guid usuarioId, string rolRequerido)
    {
        var usuario = context.Set<Usuario>().Find(usuarioId);
        if (usuario == null) return false;

        return usuario.Rol == rolRequerido;
    }
}