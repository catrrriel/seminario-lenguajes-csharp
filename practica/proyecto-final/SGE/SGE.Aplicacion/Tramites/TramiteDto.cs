using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;
public record class TramiteDto(
    Guid Id, 
    Guid ExpedienteId, 
    EtiquetaTramite Etiqueta, 
    string Contenido, 
    DateTime FechaCreacion
);
