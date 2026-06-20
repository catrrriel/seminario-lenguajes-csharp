using SGE.Aplicacion.Abstracciones;
using SGE.Infraestructura.Datos;

namespace SGE.Infraestructura.UnidadDeTrabajo;
public class UnidadDeTrabajo : IUnidadDeTrabajo
{
    private readonly SgeContext _context;

    // Inyectar el contexto de la base de datos por constructor
    public UnidadDeTrabajo(SgeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void GuardarCambios()
    {
        _context.SaveChanges();
    }
}