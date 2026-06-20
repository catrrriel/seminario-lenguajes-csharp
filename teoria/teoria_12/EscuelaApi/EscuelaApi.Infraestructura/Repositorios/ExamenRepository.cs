using EscuelaApi.Aplicacion;
using EscuelaApi.Dominio;


namespace EscuelaApi.Infraestructura;

public class ExamenRepository : Repository<Examen>, IExamenRepository
{
    public ExamenRepository(EscuelaContext context) : base(context) { }

    public IEnumerable<Examen> ObtenerPorAlumno(Guid alumnoId)
    {
        // Acá usamos el .ToList() materializador 
        return _context.Set<Examen>().Where(e => e.AlumnoId == alumnoId).ToList();
    }

    public bool ExisteExamenDeMateria(Guid alumnoId, string materia)
    {
        // El .Any() genera un SELECT EXISTS súper optimizado en SQLite
        return _context.Set<Examen>().Any(e => e.AlumnoId == alumnoId && e.Materia == materia);
    }
}