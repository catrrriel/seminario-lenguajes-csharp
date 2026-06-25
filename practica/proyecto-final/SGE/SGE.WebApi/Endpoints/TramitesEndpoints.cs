using System.Security.Claims;
using SGE.Aplicacion.Tramites;

namespace SGE.WebApi.Endpoints;
public static class TramitesEndpoints
{
    public static void MapTramitesEndpoints(this IEndpointRouteBuilder app)
    {
        var tramitesApi = app.MapGroup("/api/tramites").WithTags("Gestion de Tramites");

        // ALTA
        tramitesApi.MapPost("/", (
            AgregarTramiteRequest request,
            ClaimsPrincipal user,
            AgregarTramiteUseCase useCase) =>
        {
            // Extraemos el id del usuario ejecutor del token
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);

            var response = useCase.Ejecutar(request, idUsuario);

            return Results.Created($"/api/tramites/{response.Id}", response);
        }).RequireAuthorization();

        // BAJA
        tramitesApi.MapDelete("/{id:guid}",(
            Guid id,
            ClaimsPrincipal user,
            EliminarTramiteUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);

            var request = new EliminarTramiteRequest(id);
            var response = useCase.Ejecutar(request, idUsuario);
            
            // No Content => 204 (tambien podria ser 200 Ok, con el ID del recurso eliminado en el response)
            return Results.NoContent();
        }).RequireAuthorization();

        // MODIFICAR
        tramitesApi.MapPut("/{id:guid}",(
            Guid id,
            ModificarTramiteRequest request,
            ClaimsPrincipal user,
            ModificarTramiteUseCase useCase) =>
        {
            if(id != request.Id)
                return Results.BadRequest("El ID de la URL no coincide con el del Body");
            
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);

            var response = useCase.Ejecutar(request, idUsuario);

            return Results.Ok(response);
        }).RequireAuthorization();
    }
}