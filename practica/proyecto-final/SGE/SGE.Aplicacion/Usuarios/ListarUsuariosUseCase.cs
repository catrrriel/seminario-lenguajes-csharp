using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Comun;

namespace SGE.Aplicacion.Usuarios;
public class ListarUsuariosUseCase(IUsuarioRepository repositorio)
{
    private readonly IUsuarioRepository _repositorio = repositorio;

    public ListarUsuariosResponse Ejecutar(ListarUsuariosRequest request, Guid idUsuario)
    {
        var usuarioEjecutor = _repositorio.ObtenerPorId(idUsuario) ??
            throw new EntidadNoEncontradaException("El usuario ejecutor no existe");

        if(!usuarioEjecutor.EsAdministrador)
            throw new AutorizacionException("El usuario ejecutor no es administrados");
         
        var usuarios =_repositorio.ObtenerTodos();
        
        var dtos = usuarios.Select(u => new UsuarioDto(
            u.Id,
            u.Nombre,
            u.Email!.ToString(),
            u.EsAdministrador,
            u.Permisos
        ));

        return new ListarUsuariosResponse(dtos);
    }
}