using EscuelaApi.Aplicacion;
using EscuelaApi.Dominio;

namespace EscuelaApi.Infraestructura;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    // Pasamos el contexto a la clase base genérica
    public UsuarioRepository(EscuelaContext context) : base(context)
    {
    }

    public Usuario? ObtenerPorEmail(DireccionEmail email)
    {
        // EF Core sabe traducir esto porque compara propiedades físicas
        return _context.Set<Usuario>()
            .FirstOrDefault(u => u.Email.Cuenta == email.Cuenta && u.Email.Dominio == email.Dominio);
    }
}



