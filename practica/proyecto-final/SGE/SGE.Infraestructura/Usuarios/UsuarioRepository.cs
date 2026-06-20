using SGE.Aplicacion.Usuarios;
using SGE.Dominio.Usuarios;
using SGE.Infraestructura.Comun;
using SGE.Infraestructura.Datos;

namespace SGE.Infraestructura.Usuarios;
public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    // : base(context) => para pasarle el contexto a la clase base genérica y asi
    // obtener automaticamente los metodos basicos
    public UsuarioRepository(SgeContext context) : base(context) {}

    public Usuario? ObtenerPorEmail(string correoElectronico)
    {
        return _context.Set<Usuario>().SingleOrDefault(u => u.CorreoElectronico == correoElectronico);
    }

    public IEnumerable<Usuario> ObtenerTodos()
    {
        return _context.Set<Usuario>().ToList();
    }
}