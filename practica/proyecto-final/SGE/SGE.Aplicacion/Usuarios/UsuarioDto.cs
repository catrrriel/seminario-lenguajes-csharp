using SGE.Dominio.Autorizacion;

namespace SGE.Aplicacion.Usuarios;
public record class UsuarioDto(
    Guid Id, 
    string Nombre, 
    string Email, 
    bool EsAdministrador, 
    IEnumerable<Permiso> Permisos
);