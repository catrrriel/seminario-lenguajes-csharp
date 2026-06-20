namespace EscuelaApi.Aplicacion;

public interface IAutorizacionService
{
    // Usaremos un método bien simple con fines didácticos
    bool TieneRol(Guid usuarioId, string rolRequerido);
}
