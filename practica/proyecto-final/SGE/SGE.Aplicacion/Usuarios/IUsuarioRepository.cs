using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios;
public interface IUsuarioRepository : IRepository<Usuario>
{
    Usuario? ObtenerPorEmail(string correoElectronico);
    IEnumerable<Usuario> ObtenerTodos();
}