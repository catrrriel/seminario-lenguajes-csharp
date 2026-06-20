using System.Net;
using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Autorizacion;
using SGE.Dominio.Comun;

namespace SGE.Aplicacion.Tramites;

public class EliminarTramiteUseCase(ITramiteRepository repositorio, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService actualizacion, IUnidadDeTrabajo unidadDeTrabajo)
{
    private readonly ITramiteRepository _repositorio = repositorio;
    private readonly IAutorizacionService _autorizacion = autorizacion;
    private readonly ActualizacionEstadoExpedienteService _actualizacion = actualizacion;
    private readonly IUnidadDeTrabajo _unidadDeTrabajo = unidadDeTrabajo;

    public EliminarTramiteResponse Ejecutar(EliminarTramiteRequest request)
    {
        if(!_autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.TramiteBaja))
            throw new AutorizacionException("El usuario no tiene el permiso para eliminar tramites");
        
        var tramite = _repositorio.ObtenerPorId(request.Id) 
            ?? throw new EntidadNoEncontradaException("El tramite a eliminar ya no existe en el repositorio");
        
        _repositorio.Eliminar(tramite.Id);
        _actualizacion.Ejecutar(tramite.ExpedienteId, request.IdUsuario);
        _unidadDeTrabajo.GuardarCambios();
        return new EliminarTramiteResponse(tramite.Id); // o request.Id ?
    }
}