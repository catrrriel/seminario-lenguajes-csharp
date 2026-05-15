using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Expedientes;
using SGE.Infraestructura.Comun;

namespace SGE.Infraestructura.Expedientes;

public class ExpedienteTxtRepository : IExpedienteRepository
{
    private readonly string _rutaArchivo = "expedientes.txt";

    public void Agregar (Expediente expediente)
    {
        using var sw = new StreamWriter(_rutaArchivo, true);
        sw.WriteLine(expediente.Id);
        sw.WriteLine(expediente.Caratula.Valor);
        sw.WriteLine(expediente.FechaCreacion);
        sw.WriteLine(expediente.FechaUltimaModificacion);
        sw.WriteLine(expediente.UsuarioUltimoCambio);
        sw.WriteLine(expediente.Estado);
    }

    public IEnumerable<Expediente> ObtenerTodos()
    {
        if(!File.Exists(_rutaArchivo))
            return []; // o return Enumerable.Empty<Expediente>(); para devolver una coleccion vacia
        
        var expedientes = new List<Expediente>();
        using var sr = new StreamReader(_rutaArchivo);
        while (!sr.EndOfStream)
        {
            var Id = Guid.Parse(sr.ReadLine()!);                   // ! al final para decirle al compilador  
            var Caratula = new CaratulaExpediente(sr.ReadLine()!); // que confiamos que la linea no es null
            var FechaCreacion = DateTime.Parse(sr.ReadLine()!);
            var FechaUltimaModificacion = DateTime.Parse(sr.ReadLine()!);
            var UsuarioUltimoCambio = Guid.Parse(sr.ReadLine()!);
            var Estado = Enum.Parse<EstadoExpediente>(sr.ReadLine()!);

            expedientes.Add(Expediente.Reconstruir(Id,Caratula,FechaCreacion,FechaUltimaModificacion,UsuarioUltimoCambio,Estado));
        }

        return expedientes;
    }

    public Expediente? ObtenerPorId(Guid id)
    {
        var expedientes = ObtenerTodos();
        foreach (var e in expedientes)
        {
            if(e.Id == id)
                return e;
        }

        return null;

        // con LINQ se podria retornar 
        // return ObtenerTodos().FirstOrDefault(e => e.Id == id);
    }

    private void SobrescribirArchivo(List<Expediente> expedientes)
    {
        using var sw = new StreamWriter (_rutaArchivo, false); // false borrar el archivo viejo (sobrescribir)
        foreach (var e in expedientes)
        {
            sw.WriteLine(e.Id);
            sw.WriteLine(e.Caratula.Valor);
            sw.WriteLine(e.FechaCreacion);
            sw.WriteLine(e.FechaUltimaModificacion);
            sw.WriteLine(e.UsuarioUltimoCambio);
            sw.WriteLine(e.Estado);

        }
    }

    public void Modificar(Expediente expedienteModificado)
    {
        var expedientes = ObtenerTodos().ToList();
        int index = expedientes.FindIndex(e => e.Id == expedienteModificado.Id);

        if(index == -1)
            throw new RepositorioException("El expediente a modificar no existe en el repositorio");
        
        expedientes[index] = expedienteModificado;

        SobrescribirArchivo(expedientes);
    }

    public void Eliminar(Guid id)
    {
        var expedientes = ObtenerTodos().ToList();
        int borrados = expedientes.RemoveAll(e => e.Id == id);

        if(borrados == 0)
            throw new RepositorioException("No se encontro el expediente a eliminar");
        
        SobrescribirArchivo(expedientes);
    }
}