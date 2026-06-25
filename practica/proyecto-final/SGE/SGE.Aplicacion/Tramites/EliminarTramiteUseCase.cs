using System.Net;
using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Autorizacion;
using SGE.Dominio.Comun;

namespace SGE.Aplicacion.Tramites;

public class EliminarTramiteUseCase(ITramiteRepository repositorio, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService actualizacionEstado, IUnidadDeTrabajo unidadDeTrabajo)
{
    private readonly ITramiteRepository _repositorio = repositorio;
    private readonly IAutorizacionService _autorizacion = autorizacion;
    private readonly ActualizacionEstadoExpedienteService _actualizacionEstado = actualizacionEstado;
    private readonly IUnidadDeTrabajo _unidadDeTrabajo = unidadDeTrabajo;

    public EliminarTramiteResponse Ejecutar(EliminarTramiteRequest request, Guid idUsuario)
    {
        if(!_autorizacion.PoseeElPermiso(idUsuario, Permiso.TramiteBaja))
            throw new AutorizacionException("El usuario no tiene el permiso para eliminar tramites");
        
        var tramite = _repositorio.ObtenerPorId(request.Id) 
            ?? throw new EntidadNoEncontradaException("El tramite a eliminar ya no existe en el repositorio");
        
        _repositorio.Eliminar(tramite.Id);
        _unidadDeTrabajo.GuardarCambios();
        _actualizacionEstado.Ejecutar(tramite.ExpedienteId, idUsuario);
        _unidadDeTrabajo.GuardarCambios();
        return new EliminarTramiteResponse(tramite.Id);
    }
}