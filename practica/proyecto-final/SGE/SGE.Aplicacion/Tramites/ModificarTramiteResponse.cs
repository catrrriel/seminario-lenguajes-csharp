using SGE.Dominio.Tramites;
namespace SGE.Aplicacion.Tramites;
public record class ModificarTramiteResponse(
    Guid Id, 
    EtiquetaTramite Etiqueta, 
    string Contenido
);
