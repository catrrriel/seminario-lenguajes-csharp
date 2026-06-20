using EscuelaApi.Dominio;
using Microsoft.EntityFrameworkCore;

namespace EscuelaApi.Infraestructura;

public class EscuelaSqlite
{
    // Ahora recibe el contexto inyectado desde el exterior
    public static void Inicializar(EscuelaContext context)
    {
        if (context.Database.EnsureCreated())
        {
            Console.WriteLine("Se creó base de datos");
            
            // Establecemos la propiedad journal_mode de la base de datos SQLite en DELETE 
            var connection = context.Database.GetDbConnection();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA journal_mode=DELETE;";
                command.ExecuteNonQuery();
            }

            // Cargamos algunos datos de prueba
            var juan = new Alumno("Juan", new DireccionEmail("juan", "gmail.com"));
            var ana = new Alumno("Ana", new DireccionEmail("ana", "gmail.com"));
            var laura = new Alumno("Laura", new DireccionEmail("laura", "hotmail.com"));

            context.Alumnos.Add(juan);
            context.Alumnos.Add(ana);
            context.Alumnos.Add(laura);

            // ¡Magia de los Guids en acción! 
            // ana.Id y juan.Id ya existen y son válidos incluso antes de ir a la base de datos.
            context.Examenes.Add(new Examen(ana.Id, "Inglés", 9, new DateTime(2022, 4, 4)));
            context.Examenes.Add(new Examen(juan.Id, "Inglés", 5, new DateTime(2019, 3, 1)));
            context.Examenes.Add(new Examen(juan.Id, "Álgebra", 10, new DateTime(2021, 5, 24)));

            //Cargamos los usuarios de prueba para la seguridad
            var admin = new Usuario("Admin del Sistema", new DireccionEmail("admin", "escuela.edu.ar"), "Administrador","admin123");
            var profe = new Usuario("Roberto (Profesor)", new DireccionEmail("roberto", "escuela.edu.ar"), "Profesor","profe123");

            context.Usuarios.Add(admin);
            context.Usuarios.Add(profe);

            context.SaveChanges();
        }
    }
}