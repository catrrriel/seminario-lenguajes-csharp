using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Comun;

namespace SGE.Aplicacion.Usuarios;
public class ListarUsuariosUseCase(IUsuarioRepository repositorio)
{
    private readonly IUsuarioRepository _repositorio = repositorio;

    public IEnumerable<UsuarioResponse> Ejecutar(ListarUsuariosRequest request)
    {
        var usuario = _repositorio.ObtenerPorId(request.IdUsuario) ?? 
            throw new EntidadNoEncontradaException("El usuario ejecutor no existe.");
        
        if (!usuario.EsAdministrador)
            throw new AutorizacionException("El usuario ejecutor no es administrador.");
        
        return _repositorio.ObtenerTodos().Select(u => new UsuarioResponse(
            u.Id,
            u.Nombre,
            u.Email!.ToString(),
            u.EsAdministrador,
            u.Permisos
        ));
    }
}