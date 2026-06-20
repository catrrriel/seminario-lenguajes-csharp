using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Autorizacion;
using SGE.Dominio.Comun;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class ModificarTramiteUseCase(ITramiteRepository repositorio, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService actualizacionEstado, IUnidadDeTrabajo unidadDeTrabajo)
{
    private readonly ITramiteRepository _repositorio = repositorio;
    private readonly IAutorizacionService _autorizacion = autorizacion;
    private readonly ActualizacionEstadoExpedienteService _actualizacionEstado = actualizacionEstado;
    private readonly IUnidadDeTrabajo _unidadDeTrabajo = unidadDeTrabajo;
    public ModificarTramiteResponse Ejecutar(ModificarTramiteRequest request)
    {
        if(!_autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.TramiteModificacion))
            throw new AutorizacionException("El usuario no tiene permiso para modificar tramites");
        
        var tramite = _repositorio.ObtenerPorId(request.Id)
            ?? throw new EntidadNoEncontradaException("El tramite no existe en el repositorio");
        
        var nuevoContenido = new ContenidoTramite(request.NuevoContenido);
        tramite.ModificarContenido(nuevoContenido, request.IdUsuario);
        tramite.CambiarEtiqueta(request.NuevaEtiqueta, request.IdUsuario);

        _repositorio.Modificar(tramite);
        _actualizacionEstado.Ejecutar(tramite.ExpedienteId, request.IdUsuario);
        _unidadDeTrabajo.GuardarCambios();
        return new ModificarTramiteResponse(tramite.Id, tramite.Etiqueta, tramite.Contenido.Valor);
    }
}