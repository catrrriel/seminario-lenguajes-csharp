 
using EscuelaApi.Dominio;

namespace EscuelaApi.Aplicacion;

public interface IExamenRepository : IRepository<Examen>
{
    // 1. Para mostrar Ruteo Anidado en la API
    IEnumerable<Examen> ObtenerPorAlumno(Guid alumnoId);
    
    // 2. Para disparar el Filtro de Errores (Middleware) en la API
    bool ExisteExamenDeMateria(Guid alumnoId, string materia);
}