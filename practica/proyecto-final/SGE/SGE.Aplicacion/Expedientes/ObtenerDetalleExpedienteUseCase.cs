using SGE.Aplicacion.Tramites;
using SGE.Dominio.Comun;

namespace SGE.Aplicacion.Expedientes;
public class ObtenerDetalleExpedienteUseCase(IExpedienteRepository expedienteRepositorio, ITramiteRepository tramiteRepositorio)
{
    private readonly IExpedienteRepository _expedienteRepositorio = expedienteRepositorio;
    private readonly ITramiteRepository _tramiteRepositorio = tramiteRepositorio;

    public ObtenerDetalleExpedienteResponse Ejecutar(ObtenerDetalleExpedienteRequest request)
    {
        var expediente = _expedienteRepositorio.ObtenerPorId(request.Id)
            ?? throw new EntidadNoEncontradaException("El expediente no existe.");
        var tramites = _tramiteRepositorio.ObtenerPorExpedienteId(expediente.Id);

        // Mapeamos a Dto
        var tramitesDto = tramites.Select(t => new TramiteDto(
            t.Id,
            t.ExpedienteId,
            t.Etiqueta,
            t.Contenido.Valor,
            t.FechaCreacion)).ToList();

        return new ObtenerDetalleExpedienteResponse(expediente.Id, expediente.Caratula.Valor, expediente.Estado.ToString(), tramitesDto);
    }
}