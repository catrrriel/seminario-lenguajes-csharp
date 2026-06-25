using SGE.Dominio.Tramites;
namespace SGE.Aplicacion.Tramites;
public record class AgregarTramiteRequest(
    Guid ExpedienteID, 
    EtiquetaTramite Etiqueta, 
    string Contenido
);
