namespace SGE.Aplicacion.Expedientes;

public class ListarExpedientesUseCase(IExpedienteRepository repositorio)
{
    private readonly IExpedienteRepository _repositorio = repositorio;

    public ListarExpedientesResponse Ejecutar(ListarExpedientesRequest request)
    {
        var expedientes = _repositorio.ObtenerTodos();
        
        var dtos = expedientes.Select(e => new ExpedienteDto(
            e.Id,
            e.Caratula.Valor,
            e.Estado,
            e.FechaCreacion,
            e.FechaUltimaModificacion
        ));

        return new ListarExpedientesResponse(dtos);
    }
}