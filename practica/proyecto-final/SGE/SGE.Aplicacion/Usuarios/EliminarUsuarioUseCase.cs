using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Comun;

namespace SGE.Aplicacion.Usuarios;
public class EliminarUsuarioUseCase(IUsuarioRepository repositorio, IUnidadDeTrabajo unidadDeTrabajo)
{
    private readonly IUsuarioRepository _repositorio = repositorio;
    private readonly IUnidadDeTrabajo _unidadDeTrabajo = unidadDeTrabajo;

    public EliminarUsuarioResponse Ejecutar(EliminarUsuarioRequest request)
    {
        var usuarioEjecutor = _repositorio.ObtenerPorId(request.IdUsuarioEjecutor) ??
            throw new EntidadNoEncontradaException("El usuario ejecutor no existe");
        if (!usuarioEjecutor.EsAdministrador)
            throw new AutorizacionException("El usuario ejecutor no es administrados.");
        
        var usuarioAEliminar = _repositorio.ObtenerPorId(request.IdUsuarioAEliminar) ??
            throw new EntidadNoEncontradaException("El usuario a eliminar no existe.");
        _repositorio.Eliminar(usuarioAEliminar.Id);

        _unidadDeTrabajo.GuardarCambios();

        return new EliminarUsuarioResponse(usuarioAEliminar.Id);
    }
}