using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Autorizacion;

namespace SGE.Infraestructura.Autorizacion;

public class AutorizacionProvisionalService : IAutorizacionService
{
    public bool PoseeElPermiso(Guid idUsuario, Permiso permiso) => true;
}