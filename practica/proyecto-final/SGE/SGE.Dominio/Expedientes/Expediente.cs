using SGE.Dominio.Comunes;

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

}