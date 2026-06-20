using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Autorizacion;
using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

public class AgregarExpedienteUseCase(IExpedienteRepository repositorio, IAutorizacionService autorizacion, IUnidadDeTrabajo unidadDeTrabajo)
{
    private readonly IExpedienteRepository _repositorio = repositorio;
    private readonly IAutorizacionService _autorizacion = autorizacion;
    private readonly IUnidadDeTrabajo _unidadDeTrabajo = unidadDeTrabajo;

    public AgregarExpedienteResponse Ejecutar(AgregarExpedienteRequest request)
    {
        // verificar permiso
        if (!_autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteAlta))
            throw new AutorizacionException("El usuario no tiene permiso para crear expedientes.");

        // construir value object  e instanciar entidad desde los datos planos del request
        var caratula = new CaratulaExpediente(request.Caratula);
        var expediente = new Expediente(caratula, request.IdUsuario);

        // persistir
        _repositorio.Agregar(expediente);
        _unidadDeTrabajo.GuardarCambios();
        // devolver DTO con los datos resultantes
        return new AgregarExpedienteResponse(expediente.Id, expediente.Caratula.Valor, expediente.Estado);
    }
}