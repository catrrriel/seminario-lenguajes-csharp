using EscuelaApi.Aplicacion;

namespace EscuelaApi.Infraestructura;

public class UnidadDeTrabajo : IUnidadDeTrabajo
{
    private readonly EscuelaContext _context;

    public UnidadDeTrabajo(EscuelaContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void GuardarCambios()
    {
        // EF Core agrupa automáticamente todas las operaciones de inserción, 
        // modificación y eliminación (Change Tracker) y las ejecuta en 
        // una única transacción atómica en la base de datos.
        _context.SaveChanges();
    }
}

