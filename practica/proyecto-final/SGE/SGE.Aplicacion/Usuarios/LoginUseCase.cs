using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios;
public class LoginUseCase(IUsuarioRepository repositorio, IHashService hashService, ITokenProvider tokenProvider)
{
    private readonly IUsuarioRepository _repositorio = repositorio;
    private readonly IHashService _hashService = hashService;
    private readonly ITokenProvider _tokenProvider = tokenProvider;

    public LoginResponse Ejecutar(LoginRequest request)
    {
        var email = DireccionEmail.Parse(request.Email);
        var usuario = _repositorio.ObtenerPorEmail(email);
        string hashIngresado = _hashService.ObtenerHash(request.Contrasena);

        if(usuario == null || usuario.ContrasenaHash != hashIngresado)
            throw new AutorizacionException("Email o contraseña incorrectas");

        return new LoginResponse(_tokenProvider.GenerarToken(usuario));
    }
}