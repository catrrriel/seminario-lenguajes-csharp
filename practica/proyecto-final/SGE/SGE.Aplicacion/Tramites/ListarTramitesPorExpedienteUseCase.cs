namespace SGE.Aplicacion.Tramites;

public class ListarTramitesPorExpedienteUseCase(ITramiteRepository repositorio)
{
    private readonly ITramiteRepository _repositorio = repositorio;

    public IEnumerable<TramiteResponse> Ejecutar(ListarTramitesPorExpedienteRequest request)
    {
        return _repositorio.ObtenerPorExpedienteId(request.ExpedienteId)
            .Select(t => new TramiteResponse(
                t.Id,
                t.ExpedienteId,
                t.Etiqueta,
                t.Contenido.Valor,
                t.FechaCreacion
            ));
    }
}