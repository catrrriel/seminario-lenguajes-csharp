using Microsoft.EntityFrameworkCore;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;
using SGE.Dominio.Usuarios;

namespace SGE.Infraestructura.Datos;
public class SgeContext : DbContext
{
    public DbSet<Expediente> Expedientes { get; set; }
    public DbSet<Tramite> Tramites { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    // Constructor para recibir la configuracion
    public SgeContext(DbContextOptions<SgeContext> options):base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Expediente>().ComplexProperty(e => e.Caratula);
        modelBuilder.Entity<Tramite>().ComplexProperty(t => t.Contenido);
        modelBuilder.Entity<Usuario>().ComplexProperty(u => u.Email);
    }
}