using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Comun;

namespace SGE.Aplicacion.Usuarios;
public class ModificarPermisosUsuarioUseCase(IUsuarioRepository repositorio, IUnidadDeTrabajo unidadDeTrabajo)
{
    private readonly IUsuarioRepository _repositorio = repositorio;
    private readonly IUnidadDeTrabajo _unidadDeTrabajo = unidadDeTrabajo;

    public ModificarPermisosUsuarioResponse Ejecutar (ModificarPermisosUsuarioRequest request)
    {
        var usuarioEjecutor = _repositorio.ObtenerPorId(request.IdUsuarioEjecutor) ??
            throw new EntidadNoEncontradaException("El usuario ejecutor no existe");
        if (!usuarioEjecutor.EsAdministrador)
            throw new AutorizacionException("El usuario ejecutor no es administrador.");

        var usuarioAModificar = _repositorio.ObtenerPorId(request.IdUsuarioAModificar) ??
            throw new EntidadNoEncontradaException("El usuario a modificar no existe");
        
        // No se puede modificar una coleccion mientras la estas recorriendo
        // por eso ToList() nos permite ir recorriendo una copia de los permisos.
        // Entonces podemos eliminar elementos de la lista original sin romper el programa.
        foreach (var permiso in usuarioAModificar.Permisos.ToList())
        {
            if(!request.Permisos.Contains(permiso))   // Si el actual no esta en la nueva lista que mando el admin
                usuarioAModificar.RemoverPermiso(permiso);
        }

        // Para asignar nuevos permisos
        foreach (var permiso in request.Permisos)
        {
            usuarioAModificar.AsignarPermiso(permiso);
        }

        _unidadDeTrabajo.GuardarCambios();
        return new ModificarPermisosUsuarioResponse(usuarioAModificar.Id);
    }
}