namespace Hosting.Aplicacion;
public class LoggerConsola : ILogger
{
   public void Log(string mensaje)
   {
      Console.WriteLine($"{DateTime.Now:HH:mm:ss:fff}  {mensaje}");
   }
}