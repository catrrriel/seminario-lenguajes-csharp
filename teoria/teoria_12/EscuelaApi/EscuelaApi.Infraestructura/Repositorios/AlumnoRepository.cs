using EscuelaApi.Aplicacion;
using EscuelaApi.Dominio;

namespace EscuelaApi.Infraestructura;

public class AlumnoRepository : Repository<Alumno>, IAlumnoRepository
{
    public AlumnoRepository(EscuelaContext context) : base(context)
    {
    }
    // Se completaría con miembros específicos de AlumnoRepository
}
