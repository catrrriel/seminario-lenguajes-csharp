using SGE.Dominio.Comun;
using SGE.Dominio.Tramites;

namespace SGE.Dominio.Expedientes;
public class Expediente
{
    public Guid Id { get; private set; }
    public CaratulaExpediente Caratula { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public DateTime FechaUltimaModificacion { get; private set; }
    public Guid UsuarioUltimoCambio { get; private set; }
    public EstadoExpediente Estado{ get; private set; }

    public Expediente(CaratulaExpediente caratula, Guid idUsuario)
        :this(Guid.NewGuid(), caratula, DateTime.Now, DateTime.Now, idUsuario, EstadoExpediente.RecienIniciado)
    {
    }
    
    private Expediente(Guid id, CaratulaExpediente caratula, DateTime fechaCreacion,
                       DateTime fechaUltimaModificacion, Guid idUsuario, EstadoExpediente estado)
    {
        if(idUsuario == Guid.Empty)
            throw new DominioException("El id de usuario no puede estar vacio.");
        if(fechaUltimaModificacion < fechaCreacion)
            throw new DominioException("La fecha de la ultima modificacion no puede ser anterior a la de creacion.");
        
        Id = id;
        Caratula = caratula ?? throw new DominioException("La caratula no puede ser nula.");
        FechaCreacion = fechaCreacion;
        FechaUltimaModificacion = fechaUltimaModificacion;
        UsuarioUltimoCambio = idUsuario;
        Estado = estado;
    }

    public static Expediente Reconstruir(Guid id, CaratulaExpediente caratula, DateTime fechaCreacion,
                                         DateTime fechaUltimaModificacion, Guid idUsuario, EstadoExpediente estado)
        => new Expediente(id, caratula, fechaCreacion, fechaUltimaModificacion, idUsuario, estado);

    public void ModificarCaratula(CaratulaExpediente nuevaCaratula, Guid idUsuario)
    {
        if(idUsuario == Guid.Empty)
            throw new DominioException("El id de usuario no puede estar vacio.");
        Caratula = nuevaCaratula ?? throw new DominioException("La nueva caratula no puede ser nula");
        UsuarioUltimoCambio = idUsuario;
        FechaUltimaModificacion = DateTime.Now;
    }

    public void CambiarEstado(EstadoExpediente nuevoEstado, Guid idUsuario)
    {
        if(idUsuario == Guid.Empty)
            throw new DominioException("El id de usuario no puede estar vacio.");
        if(nuevoEstado == Estado)
            throw new DominioException($"El estado del expediente ya se encuentra en {nuevoEstado}");
        
        // DUDA
        //if (!Enum.IsDefined(typeof(EstadoExpediente), nuevoEstado))
        //    throw new DominioException("El estado indicado no es válido.");

        Estado = nuevoEstado;
        UsuarioUltimoCambio = idUsuario;
        FechaUltimaModificacion = DateTime.Now;
    }

    public bool ActualizarEstado(EtiquetaTramite? ultimaEtiqueta, Guid idUsuario)
    {
        if(idUsuario == Guid.Empty)
            throw new DominioException("El id de usuario no puede estar vacio.");
        
        EstadoExpediente nuevoEstado = ultimaEtiqueta switch
        {
            EtiquetaTramite.PaseAEstudio => EstadoExpediente.ParaResolver,
            EtiquetaTramite.Resolucion => EstadoExpediente.ConResolucion,
            EtiquetaTramite.PaseAlArchivo => EstadoExpediente.Finalizado,
            null => EstadoExpediente.RecienIniciado,
            _ => Estado // una etiqueta sin mapeo (Despacho/EscritoPresentado)no cambia el estado
        };

        if(nuevoEstado == Estado)
            return false;
        
        Estado = nuevoEstado;
        UsuarioUltimoCambio = idUsuario;
        FechaUltimaModificacion = DateTime.Now;
        return true;
    }

}