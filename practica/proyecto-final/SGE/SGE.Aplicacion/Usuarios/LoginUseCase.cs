using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios;
public class LoginUseCase(IUsuarioRepository repositorio, IHashService hashService, ITokenProvider tokenProvider)
{
    private readonly IUsuarioRepository _repositorio = repositorio;
    private readonly IHashService _hashService = hashService;
    private readonly ITokenProvider _tokenProvider = tokenProvider;

    public LoginResponse Ejecutar(LoginRequest request)
    {
        DireccionEmail email;
        // Delegamos la validacion al dominio intentando crear el ValueObject
        try
        {
            email = DireccionEmail.Parse(request.Email);
        }
        catch(DominioException) // Atrapamos la regla de dominio rota
        {
            throw new AutorizacionException("El formato de la contraseña o el email son incorrectos.");
        }
        var usuario = _repositorio.ObtenerPorEmail(email);
        string hashIngresado = _hashService.ObtenerHash(request.Contrasena);

        // Validamos credenciales
        if(usuario == null || usuario.ContrasenaHash != hashIngresado)
            throw new AutorizacionException("Email o contraseña incorrectas");

        return new LoginResponse(_tokenProvider.GenerarToken(usuario));
    }
}