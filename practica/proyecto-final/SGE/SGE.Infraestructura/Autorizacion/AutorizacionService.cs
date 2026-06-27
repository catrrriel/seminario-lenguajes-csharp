using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Usuarios;
using SGE.Dominio.Autorizacion;

namespace SGE.Infraestructura.Autorizacion;

public class AutorizacionService(IUsuarioRepository repositorio) : IAutorizacionService
{
    private readonly IUsuarioRepository _repositorio = repositorio;

    public bool PoseeElPermiso(Guid idUsuario, Permiso permiso)
    {
        var usuario = _repositorio.ObtenerPorId(idUsuario);

        // Si no existe el usuario no otorga permiso
        if(usuario == null)
            return false;
        
        if(usuario.EsAdministrador)
            return true;

        // Si tiene ExpedienteBaja, por implicancia se otorga TramiteBaja
        if(permiso == Permiso.TramiteBaja && usuario.Permisos.Contains(Permiso.ExpedienteBaja))
            return true;
        
        return usuario.Permisos.Contains(permiso);
    }
}