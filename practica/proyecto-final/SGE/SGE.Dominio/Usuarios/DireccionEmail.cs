using SGE.Dominio.Comun;

namespace SGE.Dominio.Usuarios;
public record class DireccionEmail
{
    public string Cuenta {get; private init;} = "";
    public string Dominio {get; private init;} = "";

    public DireccionEmail(string cuenta, string dominio)
    {
        if(string.IsNullOrWhiteSpace(cuenta) || string.IsNullOrWhiteSpace(dominio))
        {
            throw new DominioException("La cuenta y el dominio son obligatorios en la direccion email.");
        }
        Cuenta = cuenta;
        Dominio = dominio;
    }

    protected DireccionEmail() { }

    // Metodo de fabrica para instanciar a partir de un string completo
    public static DireccionEmail Parse(string email)
    {
        if(string.IsNullOrWhiteSpace(email) || !email.Contains('@') || !email.Contains('.'))
        {
            throw new DominioException("El formato del email es invalido");
        }

        // Validacion por strings malformados como "usuario@" o "@dominio.com"
        var partes = email.Split('@');
        if(string.IsNullOrWhiteSpace(partes[0]) || string.IsNullOrWhiteSpace(partes[1]))
        {
            throw new DominioException("El formato del email es invalido");
        }

        return new DireccionEmail(partes[0], partes[1]);
    }

    public override string ToString()
    {
        return $"{Cuenta}@{Dominio}";
    }
}