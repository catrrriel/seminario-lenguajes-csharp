using SGE.Dominio.Comun;
using SGE.Dominio.Tramites;

namespace SGE.Dominio.Expedientes;
public class Expediente : Entidad
{
    public CaratulaExpediente Caratula { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public DateTime FechaUltimaModificacion { get; private set; }
    public Guid UsuarioUltimoCambio { get; private set; }
    public EstadoExpediente Estado{ get; private set; }

    public Expediente(CaratulaExpediente caratula, Guid idUsuario)
    {
        if(idUsuario == Guid.Empty)
            throw new DominioException("El id de usuario no puede estar vacio.");
    
        Id = Guid.NewGuid();
        Caratula = caratula ?? throw new DominioException("La caratula no puede ser nula.");
        UsuarioUltimoCambio = idUsuario;
        FechaCreacion = DateTime.Now;
        FechaUltimaModificacion = DateTime.Now;
        Estado = EstadoExpediente.RecienIniciado;
    }

    protected Expediente()
    {
        Caratula = null!; // Para decirle que caratula no va a ser null, EF Core llena el dato por reflexion
    }

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
            return;
        
        // DUDA
        if (!Enum.IsDefined(typeof(EstadoExpediente), nuevoEstado))
            throw new DominioException("El estado indicado no es valido.");

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
            EtiquetaTramite.Notificacion => EstadoExpediente.EnNotificacion,
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