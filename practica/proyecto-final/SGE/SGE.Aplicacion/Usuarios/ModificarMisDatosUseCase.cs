using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios;
public class ModificarMisDatosUseCase(IUsuarioRepository repositorio, IHashService hashService, IUnidadDeTrabajo unidadDeTrabajo)
{
    private readonly IUsuarioRepository _repositorio = repositorio;
    private readonly IHashService _hashService = hashService;
    private readonly IUnidadDeTrabajo _unidadDeTrabajo = unidadDeTrabajo;

    public ModificarMisDatosResponse Ejecutar(ModificarMisDatosRequest request, Guid idUsuario)
    {
        // Buscar usuario por la Id extraida del token
        var usuario = _repositorio.ObtenerPorId(idUsuario) ??
            throw new EntidadNoEncontradaException("El usuario no existe.");
        
        // Actualizar datos
        var nuevoEmail = DireccionEmail.Parse(request.NuevoEmail);
        usuario.ModificarDatos(request.NuevoNombre, nuevoEmail);

        // Si llega una contraseña la actualizamos
        if (!string.IsNullOrWhiteSpace(request.NuevaContrasena))
        {
            string nuevoHash = _hashService.ObtenerHash(request.NuevaContrasena);
            usuario.ModificarContrasena(nuevoHash);
        }

        _unidadDeTrabajo.GuardarCambios();
        // null-forgiving al email porque ya le asignamos valor arriba
        return new ModificarMisDatosResponse(usuario.Id, usuario.Nombre, usuario.Email!.ToString()); 
    }
}