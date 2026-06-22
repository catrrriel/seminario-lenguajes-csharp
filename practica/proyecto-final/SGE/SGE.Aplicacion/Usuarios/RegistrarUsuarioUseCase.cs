using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios;
public class RegistrarUsuarioUseCase(IUsuarioRepository repositorio, IHashService hashService, IUnidadDeTrabajo unidadDeTrabajo )
{
    private readonly IUsuarioRepository _repositorio = repositorio;
    private readonly IHashService _hashService = hashService;
    private readonly IUnidadDeTrabajo _unidadDeTrabajo = unidadDeTrabajo;

    public RegistrarUsuarioResponse Ejecutar(RegistrarUsuarioRequest request)
    {
        var email = DireccionEmail.Parse(request.Email);

        if(_repositorio.ObtenerPorEmail(email) != null)
            throw new DominioException("El correo electronico ya se encuentra registrado");
        
        string contraseñaHash = _hashService.ObtenerHash(request.Contrasena);

        var usuario = new Usuario (request.Nombre, email, contraseñaHash);

        _repositorio.Agregar(usuario);
        _unidadDeTrabajo.GuardarCambios();

        return new RegistrarUsuarioResponse(usuario.Id);
    }
}