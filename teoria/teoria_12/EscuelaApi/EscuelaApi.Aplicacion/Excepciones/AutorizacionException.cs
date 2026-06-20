
namespace EscuelaApi.Aplicacion;

public class AutorizacionException : Exception
{
    public AutorizacionException()
    {
    }

    public AutorizacionException(string mensaje) : base(mensaje) { }
    
    public AutorizacionException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    
}