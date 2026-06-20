namespace EscuelaApi.Dominio;

public class Alumno : Entidad
{
    public string Nombre { get; private set; } = "";

    public DireccionEmail? Email { get; private set; }

    public Alumno(string nombre, DireccionEmail? email = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new DominioException("El nombre no puede ser nulo ni estar vacío");
        }

        Nombre = nombre;
        Email = email;

    }
    protected Alumno() { } // Constructor vacío (lo utilizará EntityFramework)

    // Otros métodos de la entidad Alumno que implementan su comportamiento
    // permitiendo cambiar su estado y mantener su propia consistencia
    // es decir, las invariantes de la entidad (reglas que siempre deben ser verdaderas).
}




