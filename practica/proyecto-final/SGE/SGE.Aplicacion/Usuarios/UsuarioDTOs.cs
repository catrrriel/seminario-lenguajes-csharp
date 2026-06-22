using SGE.Dominio.Autorizacion;

namespace SGE.Aplicacion.Usuarios;

// ALTA
public record class RegistrarUsuarioRequest(string Nombre, string Email, string Contrasena);
public record class RegistrarUsuarioResponse(Guid Id);

// LOGIN
public record class LoginRequest(string Email, string Contrasena);
public record class LoginResponse(string Token);

// MODIFICAR
public record class ModificarMisDatosRequest(Guid Id, string NuevoNombre, string NuevoEmail, string? NuevaContrasena = null);
public record class ModificarMisDatosResponse(Guid Id, string Nombre, string Email);

// EXCLUSIVOS DEL ADMIN 
// LISTAR USUARIOS
public record class ListarUsuariosRequest(Guid IdUsuario);
public record class UsuarioResponse(Guid Id, string Nombre, string Email, bool EsAdministrador, IEnumerable<Permiso> Permisos);

// ELIMINAR USUARIO
public record class EliminarUsuarioRequest(Guid IdUsuarioEjecutor, Guid IdUsuarioAEliminar);
public record class EliminarUsuarioResponse(Guid Id);

// MODIFICAR PERMISOS
public record class ModificarPermisosUsuarioRequest(Guid IdUsuarioEjecutor, Guid IdUsuarioAModificar, IEnumerable<Permiso> Permisos);
public record class ModificarPermisosUsuarioResponse(Guid Id);