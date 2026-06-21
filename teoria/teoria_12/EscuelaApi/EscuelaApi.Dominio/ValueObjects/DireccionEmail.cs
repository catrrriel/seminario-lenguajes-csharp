
namespace EscuelaApi.Dominio;

public record class DireccionEmail
{
    // Inicialización en línea para satisfacer al compilador
    public string Cuenta { get; private init; } = "";
    public string Dominio { get; private init; } = "";

    public DireccionEmail(string cuenta, string dominio)
    {
        if (string.IsNullOrWhiteSpace(cuenta) || string.IsNullOrWhiteSpace(dominio))
        {
            throw new DominioException("La cuenta y el dominio son obligatorios en las direcciones de email");
        }
        Cuenta = cuenta;
        Dominio = dominio;
    }

    // Constructor vacío para EF Core (Leaking Concern)
    protected DireccionEmail() { }

        // Método de fábrica para instanciar a partir de un string completo 
    public static DireccionEmail Parse(string emailCompleto)
    {
        if (string.IsNullOrWhiteSpace(emailCompleto) || !emailCompleto.Contains('@'))
        {
            throw new DominioException("El formato del email es inválido.");
        }

        var partes = emailCompleto.Split('@');
        
        // Validación adicional por si envían strings malformados como "usuario@" o "@dominio.com"
        if (string.IsNullOrWhiteSpace(partes[0]) || string.IsNullOrWhiteSpace(partes[1]))
        {
            throw new DominioException("El formato del email es inválido.");
        }

        return new DireccionEmail(partes[0], partes[1]);
    }

    public override string ToString()
    {
        return $"{Cuenta}@{Dominio}";
    }
}

