using SGE.Aplicacion.Tramites;

namespace SGE.Aplicacion.Expedientes;
public record class ObtenerDetalleExpedienteResponse(
    Guid Id,
    string Caratula,
    string Estado,
    IEnumerable<TramiteDto> Tramites
);