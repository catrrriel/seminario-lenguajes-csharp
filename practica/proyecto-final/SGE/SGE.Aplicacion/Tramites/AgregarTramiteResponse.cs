using SGE.Dominio.Tramites;
namespace SGE.Aplicacion.Tramites;
public record class AgregarTramiteResponse(
    Guid Id, 
    EtiquetaTramite Etiqueta, 
    string Contenido
);
