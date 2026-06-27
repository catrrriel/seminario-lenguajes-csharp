using System.Security.Claims;
using SGE.Aplicacion.Usuarios;

namespace SGE.WebApi.Endpoints;
public static class UsuariosEndpoints
{
    public static void MapUsuariosEndpoints(this IEndpointRouteBuilder app)
    {
        var usuariosApi = app.MapGroup("/api/usuarios").WithTags("Gestion de Usuarios");

        // ALTA
        // No inyectamos la identidad de un usuario, ni tampoco aplicamos bloqueo de seguridad
        usuariosApi.MapPost("/", (
            RegistrarUsuarioRequest request,
            RegistrarUsuarioUseCase useCase) =>
        {
            var response = useCase.Ejecutar(request);

            return Results.Created($"/api/usuarios/{response.Id}", response);
        });

        // MODIFICAR MIS DATOS
        // En vez de que la ruta sea /{id:guid}, como una manera que sea tecnicamente imposible
        // modificar el perfil de otro usuario cambiando el id en la url
        usuariosApi.MapPut("/me", (
            ModificarMisDatosRequest request,
            ClaimsPrincipal user,
            ModificarMisDatosUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);

            var response = useCase.Ejecutar(request, idUsuario);

            return Results.Ok(response);
        }).RequireAuthorization();

        // EXCLUSIVOS DE ADMIN
        // ELIMINAR
        usuariosApi.MapDelete("/{id:guid}",(
            Guid id,
            ClaimsPrincipal user,
            EliminarUsuarioUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);

            var request = new EliminarUsuarioRequest(id);
            var response = useCase.Ejecutar(request, idUsuario);

            // No Content => 204 (tambien podria ser 200 Ok, con el ID del recurso eliminado en el response)
            return Results.NoContent();
        }).RequireAuthorization();

        // LISTAR
        usuariosApi.MapGet("/", (
            ClaimsPrincipal user,
            ListarUsuariosUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);

            var request = new ListarUsuariosRequest();
            var response = useCase.Ejecutar(request, idUsuario);

            return Results.Ok(response);
        }).RequireAuthorization();

        // MODIFICAR PERMISOS
        usuariosApi.MapPut("/{id:guid}/permisos", (
            Guid id, // idUsuarioAEliminar
            ModificarPermisosUsuarioRequest request, // Lista de permisos
            ClaimsPrincipal user, // idUsuarioEjecutor
            ModificarPermisosUsuarioUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuarioEjecutor = Guid.Parse(userIdString!);

            var response = useCase.Ejecutar(request, id, idUsuarioEjecutor);

            return Results.Ok(response);
        }).RequireAuthorization();

    }
}