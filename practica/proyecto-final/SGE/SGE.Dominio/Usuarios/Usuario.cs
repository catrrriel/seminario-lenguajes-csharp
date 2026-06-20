using System.Security.Cryptography;
using System.Text;
using SGE.Dominio.Autorizacion;
using SGE.Dominio.Comun;
namespace SGE.Dominio.Usuarios;
public class Usuario : Entidad
{
    // public Guid Id { get; private set; }
    public string Nombre { get; private set; }
    public string CorreoElectronico { get; private set; }
    public string ContrasenaHash { get; private set; }
    public bool EsAdministrador { get; private set; }
    private List<Permiso> _permisos = new List<Permiso>();
    public IReadOnlyCollection<Permiso> Permisos => _permisos.AsReadOnly();

    // Constructor vacio para EF core
    protected Usuario() { }

    public Usuario(string nombre, string correoElectronico, string contraseñaPlana)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DominioException("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(correoElectronico))
            throw new DominioException("El correo electronico es obligatorio.");

        if (string.IsNullOrWhiteSpace(contraseñaPlana))
            throw new DominioException("La contraseña es obligatoria.");

        Id = Guid.NewGuid();
        Nombre = nombre;
        CorreoElectronico = correoElectronico;
        EsAdministrador = false;
        SetContraseña(contraseñaPlana);
    }

    public void SetContraseña(string contraseñaPlana)
    {
        // Convertir el texto plano a un arreglo de bytes
        byte[] bytes = Encoding.UTF8.GetBytes(contraseñaPlana);

        // Aplicar el algoritmo sha256
        byte[] hash = SHA256.HashData(bytes);

        // Guardarlo como string hexadecimal
        ContrasenaHash = Convert.ToHexString(hash);
    }

    // Metodos para gestionar permisos de forma segura
    public void AsignarPermiso(Permiso permiso)
    {
        if (!_permisos.Contains(permiso))
        {
            _permisos.Add(permiso);
        }
    }
    public void RemoverPermiso(Permiso permiso)
    {
        _permisos.Remove(permiso);
    }

    // Metodo para que el usuario pueda modificar sus datos basicos
    public void ModificarDatos(string nombre, string correoElectronico)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DominioException("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(correoElectronico))
            throw new DominioException("El correo electronico es obligatorio.");

        Nombre = nombre;
        CorreoElectronico = correoElectronico;
    }
    
}