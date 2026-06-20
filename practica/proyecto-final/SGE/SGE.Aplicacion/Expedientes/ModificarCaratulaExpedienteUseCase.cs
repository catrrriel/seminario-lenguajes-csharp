using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Autorizacion;
using SGE.Dominio.Comun;
using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

public class ModificarCaratulaExpedienteUseCase(IExpedienteRepository repositorio, IAutorizacionService autorizacion, IUnidadDeTrabajo unidadDeTrabajo)
{
    private readonly IExpedienteRepository _repositorio = repositorio;
    private readonly IAutorizacionService _autorizacion = autorizacion;
    private readonly IUnidadDeTrabajo _unidadDeTrabajo = unidadDeTrabajo;

    public ModificarCaratulaExpedienteResponse Ejecutar(ModificarCaratulaExpedienteRequest request)
    {
        if(!_autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteModificacion))
            throw new AutorizacionException("El usuario no tiene permiso para modificar expedientes.");
        
        var expediente = _repositorio.ObtenerPorId(request.Id)
            ?? throw new EntidadNoEncontradaException("El expediente no existe en el repositorio.");
        
        var nuevaCaratula = new CaratulaExpediente(request.NuevaCaratula);
        expediente.ModificarCaratula(nuevaCaratula, request.IdUsuario);

        _repositorio.Modificar(expediente);
        _unidadDeTrabajo.GuardarCambios();
        return new ModificarCaratulaExpedienteResponse(expediente.Id, expediente.Caratula.Valor);
    }
}