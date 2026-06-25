namespace SGE.Aplicacion.Tramites;

public class ListarTramitesPorExpedienteUseCase(ITramiteRepository repositorio)
{
    private readonly ITramiteRepository _repositorio = repositorio;

    public ListarTramitesPorExpedienteResponse Ejecutar(ListarTramitesPorExpedienteRequest request)
    {
        var tramites = _repositorio.ObtenerPorExpedienteId(request.ExpedienteId);

        var dtos = tramites.Select(t => new TramiteDto(
            t.Id,
            t.ExpedienteId,
            t.Etiqueta,
            t.Contenido.Valor,
            t.FechaCreacion
        ));

        return new ListarTramitesPorExpedienteResponse(dtos);
    }
}