using SGE.Dominio.Usuarios;

namespace SGE.Infraestructura.Datos;
public class SgeSqlite
{
    public static void Inicializar(SgeContext context)
    {
        // Verificar si la db ya existe, crearla si hace falta
        // EnsureCreated devuelve true solo la primera vez que crea la base de datos
        if (context.Database.EnsureCreated())
        {
            // Admin semilla
            context.Usuarios.Add(new Usuario(
                    nombre: "Admin",
                    email: DireccionEmail.Parse("admin@sge.com"),
                    contraseñaPlana: "admin123",
                    esAdministrador: true
            ));

            // Usuario prueba con permisos
            context.Usuarios.Add(new Usuario(
                    nombre: "UsuarioConPermiso",
                    email: DireccionEmail.Parse("prueba1@sge.com"),
                    contraseñaPlana: "prueba123"
            ));
            // Usuario prueba sin permisos
            context.Usuarios.Add(new Usuario(
                    nombre: "UsuarioSinPermiso",
                    email: DireccionEmail.Parse("prueba2@sge.com"),
                    contraseñaPlana: "prueba123"
            ));

            context.SaveChanges();
        }
    }
}