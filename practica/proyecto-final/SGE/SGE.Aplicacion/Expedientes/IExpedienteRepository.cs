using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

public interface IExpedienteRepository : IRepository<Expediente>
{
    IEnumerable<Expediente> ObtenerTodos();
}