using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

// ALTA
public record class AgregarExpedienteRequest(string Caratula, Guid IdUsuario);
public record class AgregarExpedienteResponse(Guid Id, string Caratula, EstadoExpediente Estado);

// BAJA
public record class EliminarExpedienteRequest(Guid Id, Guid IdUsuario);
public record class EliminarExpedienteResponse(Guid Id);

// MODIFICAR CARATULA
public record class ModificarCaratulaExpedienteRequest(Guid Id, string NuevaCaratula, Guid IdUsuario);
public record class ModificarCaratulaExpedienteResponse(Guid Id, string Caratula);

// CAMBIAR ESTADO
public record class CambiarEstadoExpedienteRequest(Guid Id, EstadoExpediente NuevoEstado, Guid IdUsuario);
public record class CambiarEstadoExpedienteResponse(Guid Id, EstadoExpediente Estado);

// LISTAR
public record class ListarExpedientesRequest();   // DTO vacio
public record class ExpedienteResponse(Guid Id, string Caratula, EstadoExpediente Estado, DateTime FechaCreacion, DateTime FechaUltimaModificacion);