using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Autorizacion;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class AgregarTramiteUseCase(ITramiteRepository repositorio, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService actualizacionEstado, IUnidadDeTrabajo unidadDeTrabajo)
{
    private readonly ITramiteRepository _repositorio = repositorio;
    private readonly IAutorizacionService _autorizacion = autorizacion;
    private readonly ActualizacionEstadoExpedienteService _actualizacionEstado = actualizacionEstado;
    private readonly IUnidadDeTrabajo _unidadDeTrabajo = unidadDeTrabajo;

    public AgregarTramiteResponse Ejecutar (AgregarTramiteRequest request, Guid idUsuario)
    {
        if(!_autorizacion.PoseeElPermiso(idUsuario, Permiso.TramiteAlta))
            throw new AutorizacionException("El usuario no tiene permiso para agregar tramites");
        
        var contenido = new ContenidoTramite(request.Contenido);
        var tramite = new Tramite(request.ExpedienteID, request.Etiqueta, contenido, idUsuario);

        // Agregamos el Tramite al Change Tracker
        _repositorio.Agregar(tramite);
        // Guardamos fisicamente en la DB para que tenga su id definitivo
        _unidadDeTrabajo.GuardarCambios();
        // Ahora que el tramite existe en la DB, ejecutamos el servicio
        _actualizacionEstado.Ejecutar(tramite.ExpedienteId,idUsuario);
        // Guardamos la mutacion del Expediente si es que cambio el estado
        _unidadDeTrabajo.GuardarCambios();
        return new AgregarTramiteResponse(tramite.Id, tramite.Etiqueta, tramite.Contenido.Valor);
    }
}