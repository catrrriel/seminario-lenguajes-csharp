using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios;
public interface IUsuarioRepository : IRepository<Usuario>
{
    Usuario? ObtenerPorEmail(DireccionEmail email);
    IEnumerable<Usuario> ObtenerTodos();
}