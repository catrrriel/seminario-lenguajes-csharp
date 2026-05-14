using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

// MODIFICAR
public record class ModificarTramiteRequest(Guid Id, EtiquetaTramite NuevaEtiqueta, string NuevoContenido, Guid IdUsuario);
public record class ModificarTramiteResponse(Guid Id, EtiquetaTramite Etiqueta, string Contenido);

// ALTA
public record class AgregarTramiteRequest(Guid ExpedienteID, EtiquetaTramite Etiqueta, string Contenido, Guid IdUsuario);
public record class AgregarTramiteResponse(Guid Id, EtiquetaTramite Etiqueta, string Contenido);

// BAJA
public record class EliminarTramiteRequest(Guid Id, Guid IdUsuario);
public record class EliminarTramiteResponse(Guid Id);

// LISTAR
public record class ListarTramitesPorExpedienteRequest(Guid ExpedienteId);
public record class TramiteResponse(Guid Id, Guid ExpedienteId, EtiquetaTramite Etiqueta, string Contenido, DateTime FechaCreacion);
