namespace SGE.Aplicacion.Expedientes;

public class ListarExpedientesUseCase(IExpedienteRepository repositorio)
{
    private readonly IExpedienteRepository _repositorio = repositorio;

    public IEnumerable<ExpedienteResponse> Ejecutar()
    {
        var res = new List<ExpedienteResponse>();
        foreach (var e in _repositorio.ObtenerTodos())
        {
            res.Add(new ExpedienteResponse(e.Id, e.Caratula.Valor, e.Estado, e.FechaCreacion, e.FechaUltimaModificacion));
        }
        return res;
        
        //podria utilizarse .Select() para simplificar.  e = cada expediente
        // return _repositorio.ObtenerTodos()
        // .Select(e => new ExpedienteResponse(
        //     e.Id,
        //     e.Caratula.Valor,
        //     e.Estado,
        //     e.FechaCreacion,
        //     e.FechaUltimaModificacion));
    }
}