using SGE.Aplicacion.Tramites;
using SGE.Dominio.Tramites;
using SGE.Infraestructura.Comun;
using SGE.Infraestructura.Datos;

namespace SGE.Infraestructura.Tramites;
public class TramiteRepository : Repository<Tramite>, ITramiteRepository
{
    public TramiteRepository(SgeContext context) : base(context) { }

    public IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId)
    {
        return _context.Set<Tramite>().Where(t => t.ExpedienteId == expedienteId).ToList();
    }
}