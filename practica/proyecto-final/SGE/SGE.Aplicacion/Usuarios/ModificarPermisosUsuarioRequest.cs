using SGE.Dominio.Autorizacion;

namespace SGE.Aplicacion.Usuarios;
public record class ModificarPermisosUsuarioRequest(IEnumerable<Permiso> Permisos);
