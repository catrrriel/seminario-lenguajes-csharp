using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public interface ITramiteRepository : IRepository<Tramite>
{
    IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId);
}