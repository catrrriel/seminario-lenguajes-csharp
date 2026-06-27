using SGE.Dominio.Comun;

namespace SGE.Dominio.Tramites;

public class Tramite : Entidad
{
    public Guid ExpedienteId { get; private set; }
    public EtiquetaTramite Etiqueta { get; private set; }
    public ContenidoTramite Contenido { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public DateTime FechaUltimaModificacion { get; private set; }
    public Guid UsuarioUltimoCambio { get; private set; }

    public Tramite(Guid expedienteId, EtiquetaTramite etiqueta, ContenidoTramite contenido, Guid idUsuario)
    {
        if(expedienteId == Guid.Empty)
            throw new DominioException("El id del expediente no puede estar vacio.");
        Id = Guid.NewGuid();    
        ExpedienteId = expedienteId;
        Etiqueta = etiqueta;
        FechaCreacion = DateTime.Now;
        FechaUltimaModificacion = DateTime.Now;
        Contenido = contenido ?? throw new DominioException("El contenido no puede ser nulo.");
        UsuarioUltimoCambio = idUsuario;
    }

    protected Tramite()
    {
        Contenido = null!;
    }

    public void ModificarContenido(ContenidoTramite nuevoContenido, Guid idUsuario)
    {
        if (idUsuario == Guid.Empty)
            throw new DominioException("El id de usuario no puede estar vacio.");

        Contenido = nuevoContenido ?? throw new DominioException("El contenido no puede ser nulo.");
        UsuarioUltimoCambio = idUsuario;
        FechaUltimaModificacion = DateTime.Now;
    }

    public void CambiarEtiqueta(EtiquetaTramite nuevaEtiqueta, Guid idUsuario)
    {
        if (idUsuario == Guid.Empty)
            throw new DominioException("El id de usuario no puede estar vacio.");
        
        if (nuevaEtiqueta == Etiqueta)
            return; // no hace falta arrojar excepcion simplemente no hago nada

        if (!Enum.IsDefined(nuevaEtiqueta))
            throw new DominioException("La etiqueta indicada no es valida.");
    
        Etiqueta = nuevaEtiqueta;
        UsuarioUltimoCambio = idUsuario;
        FechaUltimaModificacion = DateTime.Now;
    }
}
