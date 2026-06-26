using System.Security.Cryptography;
using System.Text;
using SGE.Dominio.Autorizacion;
using SGE.Dominio.Comun;
namespace SGE.Dominio.Usuarios;
public class Usuario : Entidad
{
    public string Nombre { get; private set; } = "";
    public DireccionEmail? Email { get; private set; }
    public string ContrasenaHash { get; private set; } = "";
    public bool EsAdministrador { get; private set; }

    // Para encapsular mejor. private readonly solo protege la referencia, no impide que modifiquen los datos de adentro
    private List<Permiso> _permisos = new List<Permiso>();
    public IReadOnlyCollection<Permiso> Permisos => _permisos.AsReadOnly();

    // Constructor vacio para EF core
    protected Usuario() { }

    public Usuario(string nombre, DireccionEmail email, string contraseñaHash, bool esAdministrador = false)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DominioException("El nombre es obligatorio.");
    
        if (string.IsNullOrWhiteSpace(contraseñaHash))
            throw new DominioException("La contraseña es obligatoria.");

        Id = Guid.NewGuid();
        Nombre = nombre;
        Email = email ?? throw new DominioException("La direccion email es obligatoria.");
        EsAdministrador = esAdministrador;
        ContrasenaHash = contraseñaHash;
    }

    // Metodos para gestionar permisos de forma segura
    // public void AsignarPermiso(Permiso permiso)
    // {
    //     if (!_permisos.Contains(permiso))
    //     {
    //         _permisos.Add(permiso);
    //     }
    // }
    // public void RemoverPermiso(Permiso permiso)
    // {
    //     _permisos.Remove(permiso);
    // }

    public void ReemplazarPermisos(IEnumerable<Permiso> nuevosPermisos)
    {
        _permisos = nuevosPermisos.ToList();
    }

    // Metodo para que el usuario pueda modificar sus datos basicos  <= DUDA
    public void ModificarDatos(string nombre, DireccionEmail email)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DominioException("Ingresar el nombre es obligatorio para modificar.");
        
        Nombre = nombre;
        Email = email ?? throw new DominioException("Ingresar el correo electronico es obligatorio para modificar.");
    }
    
    public void ModificarContrasena(string nuevoHash)
    {
        ContrasenaHash = nuevoHash;
    }
}