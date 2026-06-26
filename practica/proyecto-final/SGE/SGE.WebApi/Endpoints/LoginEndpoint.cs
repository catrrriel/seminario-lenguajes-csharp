using SGE.Aplicacion.Usuarios;

namespace SGE.WebApi.Endpoints;
public static class LoginEndpoint
{
    public static void MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        // Endpoint individual de login
        app.MapPost("/api/login", (
            LoginRequest request,
            LoginUseCase useCase) =>
        {
            // El middleware de excepciones atrapa si el email o la contraseña son incorrectos
            var response = useCase.Ejecutar(request);

            return Results.Ok(response);
        }).WithTags("Autenticacion");
    
    }
}