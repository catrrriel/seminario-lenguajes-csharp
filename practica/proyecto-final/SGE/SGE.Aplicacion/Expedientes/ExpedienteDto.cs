using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;
// Representa cada Expediente individual dentro de la lista
public record class ExpedienteDto(
    Guid Id, 
    string Caratula, 
    EstadoExpediente Estado,
    DateTime FechaCreacion,
    DateTime FechaUltimaModificacion
);