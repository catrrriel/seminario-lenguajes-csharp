using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Comun;
using SGE.Infraestructura.Datos;

namespace SGE.Infraestructura.Comun;

// where T : Entidad => para asegurar que T sea una entidad valida
public class Repository<T> : IRepository<T> where T : Entidad
{
    protected readonly SgeContext _context;

    public Repository(SgeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Agregar(T entidad)
    {
        if (entidad == null)
            throw new ArgumentNullException(nameof(entidad),"La entidad no puede ser nula.");
        _context.Set<T>().Add(entidad);
    }

    public void Modificar(T entidad)
    {
        if(entidad == null)
            throw new ArgumentNullException(nameof(entidad),"La entidad no puede ser nula.");   
        _context.Set<T>().Update(entidad);
    }

    public void Eliminar(Guid id)
    {
        T? entidad = _context.Set<T>().Find(id) ?? 
            throw new RepositorioException($"No se encontro la entidad con id: {id} para eliminarla.");
        _context.Set<T>().Remove(entidad);
    }

    public T? ObtenerPorId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("El ID no puede ser vacío", nameof(id));

        return _context.Set<T>().Find(id);
    }
}