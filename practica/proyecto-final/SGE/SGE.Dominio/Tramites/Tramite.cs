using SGE.Dominio.Comunes;

namespace SGE.Dominio.Tramites;

public class Tramite
{
    public Guid Id { get; private set; }
    public Guid ExpedienteId { get; private set; }
    public EtiquetaTramite Etiqueta { get; private set; }
    public ContenidoTramite Contenido { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public DateTime FechaUltimaModificacion { get; private set; }
    public Guid UsuarioUltimoCambio { get; private set; }

    public Tramite(Guid expedienteId, EtiquetaTramite etiqueta, ContenidoTramite contenido, Guid idUsuario)
        :this(Guid.NewGuid(), expedienteId, etiqueta, contenido, DateTime.Now, DateTime.Now, idUsuario)
    {        
    }

    private Tramite(Guid id, Guid expedienteId, EtiquetaTramite etiqueta, ContenidoTramite contenido, 
                    DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid idUsuario)
    {
        if(expedienteId == Guid.Empty)
            throw new DominioException("El id del expediente no puede estar vacio.");
        if(idUsuario == Guid.Empty)
            throw new DominioException("El id del usuario no puede estar vacio.");
        if (fechaUltimaModificacion < fechaCreacion)
            throw new DominioException("La fecha de última modificación no puede ser anterior a la de creación.");

        Id = id;
        ExpedienteId = expedienteId;
        Etiqueta = etiqueta;
        Contenido = contenido ?? throw new DominioException("El contenido no puede ser nulo.");
        FechaCreacion = fechaCreacion;
        FechaUltimaModificacion = fechaUltimaModificacion;
        UsuarioUltimoCambio = idUsuario;
    }

    public static Tramite Reconstruir(Guid id, Guid expedienteId, EtiquetaTramite etiqueta, ContenidoTramite contenido,
                                      DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid idUsuario)
        => new Tramite(id, expedienteId, etiqueta, contenido, fechaCreacion, fechaUltimaModificacion, idUsuario);
}
