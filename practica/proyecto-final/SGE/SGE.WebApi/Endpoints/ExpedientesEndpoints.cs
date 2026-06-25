using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;
using System.Security.Claims;
namespace SGE.WebApi.Endpoints;
public static class ExpedientesEndpoints
{
    public static void MapExpedientesEndpoints(this IEndpointRouteBuilder app)
    {
        // Agrupamos bajo una misma ruta y ponemos una etiqueta para la documentacion
        var expedientesApi = app.MapGroup("/api/expedientes").WithTags("Gestion de Expedientes");

        // ALTA
        expedientesApi.MapPost("/",(
            AgregarExpedienteRequest request,   // Extraido del body JSON
            ClaimsPrincipal user,
            AgregarExpedienteUseCase useCase) => // Inyectado por DI
        {
            // Extraemos el id del usuario ejecutor del token
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);

            var response = useCase.Ejecutar(request, idUsuario);
            
            // Created => HTTP 201 con la ruta del nuevo recurso
            return Results.Created($"/api/expedientes/{response.Id}", response);
        }).RequireAuthorization(); // Para bloquear el acceso a usuarios sin token

        // BAJA
        expedientesApi.MapDelete("/{id:guid}",(
            Guid id,
            ClaimsPrincipal user,
            EliminarExpedienteUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);

            var request = new EliminarExpedienteRequest(id);
            var response = useCase.Ejecutar(request, idUsuario);
            
            // No Content => 204 (tambien podria ser 200 Ok, con el ID del recurso eliminado en el response)
            return Results.NoContent();
        }).RequireAuthorization(); 

        // MODIFICAR CARATULA
        expedientesApi.MapPut("/{id:guid}/caratula",(
            Guid id,
            ModificarCaratulaExpedienteRequest request,
            ClaimsPrincipal user,
            ModificarCaratulaExpedienteUseCase useCase) =>
        {
            if(id != request.Id)
                return Results.BadRequest("El ID de la URL no coincide con el del Body");
            
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);

            var response = useCase.Ejecutar(request, idUsuario);

            return Results.Ok(response);
        }).RequireAuthorization();

        // CAMBIAR ESTADO
        expedientesApi.MapPut("/{id:guid}/estado",(
            Guid id,
            CambiarEstadoExpedienteRequest request,
            ClaimsPrincipal user,
            CambiarEstadoExpedienteUseCase useCase) =>
        {
            if(id != request.Id)
                return Results.BadRequest("El ID de la URL no coincide con el del Body");

            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);

            var response = useCase.Ejecutar(request, idUsuario);
            
            return Results.Ok(response);
        }).RequireAuthorization();

        // LISTAR
        expedientesApi.MapGet("/", (ListarExpedientesUseCase useCase) =>
        {
            var request = new ListarExpedientesRequest();
            var response = useCase.Ejecutar(request);

            // HTTP 200 con el JSON de expedientes
            return Results.Ok(response);
        });

        // LISTAR TRAMITES POR EXPEDIENTE
        expedientesApi.MapGet("/{id:guid}/tramites", (
            Guid id,
            ListarTramitesPorExpedienteUseCase useCase) =>
        {
            var request = new ListarTramitesPorExpedienteRequest(id);
            var response = useCase.Ejecutar(request);

            return Results.Ok(response);
        });
    }
}