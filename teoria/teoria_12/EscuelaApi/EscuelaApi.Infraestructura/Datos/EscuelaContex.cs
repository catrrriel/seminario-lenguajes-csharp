using EscuelaApi.Dominio;
using Microsoft.EntityFrameworkCore;

namespace EscuelaApi.Infraestructura;

public class EscuelaContext : DbContext
{
    // 1. El contenedor DI inyectará las configuraciones a través del parámetro 'options'.
    // Al pasárselo a la clase base usando ": base(options)", Entity Framework Core 
    // absorbe toda la configuración que definimos en Program.cs.
    public EscuelaContext(DbContextOptions<EscuelaContext> options) : base(options)
    {
    }

    public DbSet<Alumno> Alumnos { get; set; }
    public DbSet<Examen> Examenes { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    // 2. ¡Eliminamos OnConfiguring! 
    // El contexto ya no tiene "hardcodeado" qué motor usar ni dónde está el archivo.
    // ¿De dónde saca la cadena de conexión? 
    // En Program.cs, cuando hacemos: builder.Services.AddDbContext(opt => opt.UseSqlite(cadena))
    // el contenedor empaqueta esa orden dentro del objeto 'options' que vimos en el constructor.
    // ¡Esto es Inversión de Dependencias en su máxima expresión!

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Le indicamos a EF Core que Email es un "Tipo Complejo" (Value Object)
        modelBuilder.Entity<Alumno>().ComplexProperty(a => a.Email);
        modelBuilder.Entity<Usuario>().ComplexProperty(u => u.Email);
    }
}