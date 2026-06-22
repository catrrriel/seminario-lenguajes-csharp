using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Autorizacion;
using SGE.Dominio.Comun;

namespace SGE.Aplicacion.Expedientes;

public class CambiarEstadoExpedienteUseCase(IExpedienteRepository repositorio, IAutorizacionService autorizacion, IUnidadDeTrabajo unidadDeTrabajo)
{
    private readonly IExpedienteRepository _repositorio = repositorio;
    private readonly IAutorizacionService _autorizacion = autorizacion;
    private readonly IUnidadDeTrabajo _unidadDeTrabajo = unidadDeTrabajo;

    public CambiarEstadoExpedienteResponse Ejecutar(CambiarEstadoExpedienteRequest request)
    {
        if(!_autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteModificacion))
            throw new AutorizacionException("El usuario no tiene permiso para modificar expedientes.");
        
        var expediente = _repositorio.ObtenerPorId(request.Id) 
            ?? throw new EntidadNoEncontradaException("El expediente no existe en el repositorio.");
        
        expediente.CambiarEstado(request.NuevoEstado, request.IdUsuario);

        _unidadDeTrabajo.GuardarCambios();
        return new CambiarEstadoExpedienteResponse(expediente.Id, expediente.Estado);
    }
}