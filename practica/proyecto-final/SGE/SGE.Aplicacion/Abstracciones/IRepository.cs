using SGE.Dominio.Comun;

namespace SGE.Aplicacion.Abstracciones;
public interface IRepository<T> where T : Entidad
{
    void Agregar(T entidad);
    void Eliminar(Guid id);
    T? ObtenerPorId(Guid id);
}