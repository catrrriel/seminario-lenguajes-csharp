namespace SGE.Dominio.Comun;

public class EntidadNoEncontradaException : Exception
{
    public EntidadNoEncontradaException()
    {
    }
    public EntidadNoEncontradaException(string? message) : base(message)
    {
    }
    public EntidadNoEncontradaException(string? message, Exception? innerException): base(message, innerException)
    {
    }
}