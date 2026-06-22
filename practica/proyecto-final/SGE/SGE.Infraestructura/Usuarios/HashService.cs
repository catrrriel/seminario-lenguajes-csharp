using SGE.Aplicacion.Autorizacion;
using System.Security.Cryptography;
using System.Text;

namespace SGE.Infraestructura.Usuarios;
public class HashService : IHashService
{
    public string ObtenerHash(string textoPlano)
    {
        // Convertir el texto plano a un arreglo de bytes
        byte[] bytes = Encoding.UTF8.GetBytes(textoPlano);

        // Aplicar el algoritmo sha256
        byte[] hash = SHA256.HashData(bytes);

        // Guardarlo como string hexadecimal
        return Convert.ToHexString(hash);
    }
}