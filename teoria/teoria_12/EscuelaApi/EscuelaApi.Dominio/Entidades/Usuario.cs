namespace EscuelaApi.Dominio;

public class Usuario : Entidad
{
    // Inicializamos por defecto para satisfacer al compilador
    public string Nombre { get; private set; } = "";
    public DireccionEmail Email { get; private set; } = null!;
    public string Rol { get; private set; } = "";
    public string Password { get; private set; } = "";

    public Usuario(string nombre, DireccionEmail email, string rol, string password)
    {
        // 1. Validamos las reglas invariantes del negocio
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DominioException("El nombre del usuario no puede estar vacío.");

        if (email == null)
            throw new DominioException("El email del usuario es obligatorio.");

        if (string.IsNullOrWhiteSpace(rol))
            throw new DominioException("El rol del usuario no puede estar vacío.");

        if (string.IsNullOrWhiteSpace(password))
            throw new DominioException("El password del usuario no puede estar vacío.");    

        // Opcional: Se podría incluso validar que el rol sea uno de los permitidos
        // if (rol != "Profesor" && rol != "Administrador")
        //    throw new DominioException($"El rol '{rol}' no es un rol válido en el sistema.");

        // 2. Si todo es válido, asignamos el estado
        Nombre = nombre;
        Email = email;
        Rol = rol;
        Password = password;
    }

    // Constructor sin parámetros protegido exclusivo para Entity Framework Core
    protected Usuario() { }
}