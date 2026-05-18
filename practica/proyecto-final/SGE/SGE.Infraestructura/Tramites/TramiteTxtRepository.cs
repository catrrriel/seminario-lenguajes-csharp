using SGE.Aplicacion.Tramites;
using SGE.Dominio.Tramites;
using SGE.Infraestructura.Comun;

namespace SGE.Infraestructura.Tramites;

public class TramiteTxtRepository : ITramiteRepository
{
    private readonly string _rutaArchivo = "tramites.txt";
    
    private IEnumerable<Tramite> ObtenerTodos()
    {
        if(!File.Exists(_rutaArchivo))
            return [];
        
        var tramites = new List<Tramite>();
        using var sr = new StreamReader(_rutaArchivo);
        while (!sr.EndOfStream)
        {
            var Id = Guid.Parse(sr.ReadLine()!);
            var ExpedienteId = Guid.Parse(sr.ReadLine()!);
            var Etiqueta = Enum.Parse<EtiquetaTramite>(sr.ReadLine()!);
            var Contenido = new ContenidoTramite(sr.ReadLine()!);
            var FechaCreacion = DateTime.Parse(sr.ReadLine()!);
            var FechaUltimaModificacion = DateTime.Parse(sr.ReadLine()!);
            var UsuarioUltimoCambio = Guid.Parse(sr.ReadLine()!);

            tramites.Add(Tramite.Reconstruir(Id,ExpedienteId,Etiqueta,Contenido,FechaCreacion,FechaUltimaModificacion,UsuarioUltimoCambio));
        }

        return tramites;
    }

    private void SobrescribirArchivo(List<Tramite> tramites)
    {
        using var sw = new StreamWriter (_rutaArchivo, false); // false para borrar el archivo viejo (sobrescribir)
        foreach (var t in tramites)
        {
            sw.WriteLine(t.Id);
            sw.WriteLine(t.ExpedienteId);
            sw.WriteLine(t.Etiqueta);
            sw.WriteLine(t.Contenido.Valor);
            sw.WriteLine(t.FechaCreacion);
            sw.WriteLine(t.FechaUltimaModificacion);
            sw.WriteLine(t.UsuarioUltimoCambio);
        }
    }

    public IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId)
    {
        var tramites = ObtenerTodos();

        foreach (var t in tramites)
        {
            if (t.ExpedienteId == expedienteId)
            {
                // devuelve el tramite al instante y pausa la ejecucion
                // hasta que se pida el proximo
                yield return t; 
            }
        }
    }

    public void Agregar (Tramite tramite)
    {
        using var sw = new StreamWriter(_rutaArchivo, true);
        sw.WriteLine(tramite.Id);
        sw.WriteLine(tramite.ExpedienteId);
        sw.WriteLine(tramite.Etiqueta);
        sw.WriteLine(tramite.Contenido.Valor);
        sw.WriteLine(tramite.FechaCreacion);
        sw.WriteLine(tramite.FechaUltimaModificacion);
        sw.WriteLine(tramite.UsuarioUltimoCambio);
    }

    public void Modificar(Tramite tramiteModificado)
    {
        var tramites = ObtenerTodos().ToList();
        int index = tramites.FindIndex(t => t.Id == tramiteModificado.Id);

        if(index == -1)
            throw new RepositorioException("El tramite a modificar no existe en el repositorio");
        
        tramites[index] = tramiteModificado;

        SobrescribirArchivo(tramites);
    }

    public void Eliminar(Guid id)
    {
        var tramites = ObtenerTodos().ToList();
        int borrados = tramites.RemoveAll(t => t.Id == id);

        if(borrados == 0)
            throw new RepositorioException("No se encontro el tramite a eliminar");
        
        SobrescribirArchivo(tramites);
    }

    public Tramite? ObtenerPorId(Guid id)
    {
        var tramites = ObtenerTodos();
        foreach (var t in tramites)
        {
            if(t.Id == id) return t;
        }

        return null;
        // return ObtenerTodos().FirstOrDefault(t => t.Id == id);
    }
}