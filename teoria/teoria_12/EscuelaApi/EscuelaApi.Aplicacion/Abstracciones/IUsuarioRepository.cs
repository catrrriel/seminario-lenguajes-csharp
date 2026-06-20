using EscuelaApi.Dominio;

namespace EscuelaApi.Aplicacion;

public interface IUsuarioRepository : IRepository<Usuario>
{
    // Agregamos este método específico
    Usuario? ObtenerPorEmail(DireccionEmail email); 
}