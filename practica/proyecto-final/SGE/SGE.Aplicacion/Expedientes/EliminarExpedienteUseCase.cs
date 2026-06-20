using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Autorizacion;
using SGE.Dominio.Comun;

namespace SGE.Aplicacion.Expedientes;

public class EliminarExpedienteUseCase(IExpedienteRepository expedienteRepositorio, ITramiteRepository tramiteRepositorio, IAutorizacionService autorizacion, IUnidadDeTrabajo unidadDeTrabajo)
{
    private readonly IExpedienteRepository _expedienteRepositorio = expedienteRepositorio;
    private readonly ITramiteRepository _tramiteRepositorio = tramiteRepositorio;
    private readonly IAutorizacionService _autorizacion = autorizacion;
    private readonly IUnidadDeTrabajo _unidadDeTrabajo = unidadDeTrabajo;

    public EliminarExpedienteResponse Ejecutar(EliminarExpedienteRequest request)
    {
        if(!_autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteBaja))
            throw new AutorizacionException("El usuario no tiene permiso para eliminar expedientes");
        
        var expediente = _expedienteRepositorio.ObtenerPorId(request.Id)
            ?? throw new EntidadNoEncontradaException("El expediente a eliminar ya no existe en el repositorio");

        var tramites = _tramiteRepositorio.ObtenerPorExpedienteId(request.Id);
        foreach (var tramite in tramites)
        {
            _tramiteRepositorio.Eliminar(tramite.Id);
        }

        _expedienteRepositorio.Eliminar(expediente.Id);
        _unidadDeTrabajo.GuardarCambios();
        return new EliminarExpedienteResponse(expediente.Id);
    }

}