using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Expedientes;
using SGE.Infraestructura.Comun;
using SGE.Infraestructura.Datos;

namespace SGE.Infraestructura.Expedientes;
public class ExpedienteRepository : Repository<Expediente>, IExpedienteRepository
{
    public ExpedienteRepository (SgeContext context) : base(context) {}

    public IEnumerable<Expediente> ObtenerTodos()
    {
        return _context.Set<Expediente>().ToList();
    }
}