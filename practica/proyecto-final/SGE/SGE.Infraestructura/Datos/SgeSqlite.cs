using Microsoft.EntityFrameworkCore;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Usuarios;

namespace SGE.Infraestructura.Datos;
public class SgeSqlite
{
    public static void Inicializar(SgeContext context, IHashService hashService)
    {
        // Verificar si la db ya existe, crearla si hace falta
        // EnsureCreated devuelve true solo la primera vez que crea la base de datos
        if (context.Database.EnsureCreated())
        {
            // Journal mode
            var connection = context.Database.GetDbConnection();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA journal_mode=DELETE;";
                command.ExecuteNonQuery();
            }

            // Admin semilla
            context.Usuarios.Add(new Usuario(
                    nombre: "Admin",
                    email: DireccionEmail.Parse("admin@sge.com"),
                    contraseñaHash: hashService.ObtenerHash("admin123"),
                    esAdministrador: true
            ));

            // Usuario prueba con permisos
            context.Usuarios.Add(new Usuario(
                    nombre: "UsuarioConPermiso",
                    email: DireccionEmail.Parse("prueba1@sge.com"),
                    contraseñaHash: hashService.ObtenerHash("prueba123")
            ));
            // Usuario prueba sin permisos
            context.Usuarios.Add(new Usuario(
                    nombre: "UsuarioSinPermiso",
                    email: DireccionEmail.Parse("prueba2@sge.com"),
                    contraseñaHash: hashService.ObtenerHash("prueba123")
            ));

            context.SaveChanges();
        }
    }
}