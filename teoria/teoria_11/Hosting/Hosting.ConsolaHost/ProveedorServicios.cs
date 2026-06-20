using Hosting.Aplicacion;

namespace Hosting.ConsolaHost;

class ProveedorServicios
{
  public ILogger GetLogger()
      => new LoggerConsola();
  public IServicioX GetServicioX()
      => new ServicioX(this.GetLogger());
}