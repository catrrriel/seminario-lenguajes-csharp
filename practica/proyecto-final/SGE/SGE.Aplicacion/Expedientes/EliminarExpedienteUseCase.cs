using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Comun;

namespace SGE.Aplicacion.Expedientes;

public class EliminarExpedienteUseCase(IExpedienteRepository expedienteRepositorio, ITramiteRepository tramiteRepositorio, IAutorizacionService autorizacion)
{
    private readonly IExpedienteRepository _expedienteRepositorio = expedienteRepositorio;
    private readonly ITramiteRepository _tramiteRepositorio = tramiteRepositorio;
    private readonly IAutorizacionService _autorizacion = autorizacion;

    public EliminarExpedienteResponse Ejecutar(EliminarExpedienteRequest request)
    {
        if(!_autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteBaja))
            throw new AutorizacionException("El usuario no posee permiso para eliminar expedientes");
        
        var expediente = _expedienteRepositorio.ObtenerPorId(request.Id)
            ?? throw new EntidadNoEncontradaException("El expediente no existe en el repositorio");

        var tramites = _tramiteRepositorio.ObtenerPorExpedienteId(request.Id);
        foreach (var tramite in tramites)
        {
            _tramiteRepositorio.Eliminar(tramite.Id);
        }

        _expedienteRepositorio.Eliminar(expediente.Id);

        return new EliminarExpedienteResponse(expediente.Id);
    }

}