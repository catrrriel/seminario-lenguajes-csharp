
namespace EscuelaApi.Aplicacion;

public class EntidadNoEncontradaException : Exception
{
    public EntidadNoEncontradaException()
    {
    }

    public EntidadNoEncontradaException(string mensaje) : base(mensaje)
    {
    }
    
    public EntidadNoEncontradaException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

}