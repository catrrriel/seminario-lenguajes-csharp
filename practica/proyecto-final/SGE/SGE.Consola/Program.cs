using System.Collections;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Comun;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;
using SGE.Infraestructura.Autorizacion;
using SGE.Infraestructura.Comun;
using SGE.Infraestructura.Expedientes;
using SGE.Infraestructura.Tramites;

// Instancias de infraestructura
IExpedienteRepository expedienteRepositorio = new ExpedienteTxtRepository();
ITramiteRepository tramiteRepositorio = new TramiteTxtRepository();
IAutorizacionService autorizacionService = new AutorizacionProvisionalService();
ActualizacionEstadoExpedienteService actualizacionService = new(expedienteRepositorio, tramiteRepositorio);

// casos de uso Expedientes
var agregarExpedienteUseCase = new AgregarExpedienteUseCase(expedienteRepositorio, autorizacionService);
var listarExpedientesUseCase = new ListarExpedientesUseCase(expedienteRepositorio);
var modificarCaratulaExpedienteUseCase = new ModificarCaratulaExpedienteUseCase(expedienteRepositorio, autorizacionService);
var cambiarEstadoExpedienteUseCase = new CambiarEstadoExpedienteUseCase(expedienteRepositorio, autorizacionService);
var eliminarExpedienteUseCase = new EliminarExpedienteUseCase(expedienteRepositorio, tramiteRepositorio, autorizacionService);

// casos de uso Tramites
var agregarTramiteUseCase = new AgregarTramiteUseCase(tramiteRepositorio, autorizacionService, actualizacionService);
var listarTramitesPorExpediente = new ListarTramitesPorExpedienteUseCase(tramiteRepositorio);
var modificarTramiteUseCase = new ModificarTramiteUseCase(tramiteRepositorio, autorizacionService, actualizacionService);
var eliminarTramiteUseCase = new EliminarTramiteUseCase(tramiteRepositorio, autorizacionService, actualizacionService);

Guid idUsuarioPrueba = Guid.NewGuid ();
Guid? expedienteIdUlt = null;
Guid? tramiteIdUlt = null;
bool salir = false;
while (!salir)
{
    Console.WriteLine("\n=== SGE - Sistema de Gestion de Expedientes ===");
    if (expedienteIdUlt.HasValue)
        Console.WriteLine($"[Expediente activo: {expedienteIdUlt}]");
    Console.WriteLine("   1. Agregar expediente");
    Console.WriteLine("   2. Listar expedientes");
    Console.WriteLine("   3. Modificar caratula de expediente");
    Console.WriteLine("   4. Cambiar estado de expediente");
    Console.WriteLine("   5. Eliminar expedientes");
    Console.WriteLine("   6. Agregar tramite");
    Console.WriteLine("   7. Eliminar tramite");
    Console.WriteLine("   8. Modificar tramite");
    Console.WriteLine("   9. Listar tramites por expediente");
    Console.WriteLine("   0. Salir");
    Console.Write("\nOpcion: ");
    var opcion = Console.ReadLine();

    try
    {
        switch (opcion)
        {
            case "1":
                Console.Write("Ingrese caratula del expediente a agregar: ");
                var caratulaExp = Console.ReadLine();
                var reqAgregar = new AgregarExpedienteRequest(caratulaExp, idUsuarioPrueba);
                var resAgregar = agregarExpedienteUseCase.Ejecutar(reqAgregar);
                expedienteIdUlt = resAgregar.Id;
                Console.WriteLine($"Expediente agregado. Id: {resAgregar.Id} | Caratula: {resAgregar.Caratula} | Estado: {resAgregar.Estado}");
                break;   
            case "2":
                Console.WriteLine("------------- Listado de expedientes -------------");
                var resLista = listarExpedientesUseCase.Ejecutar();
                foreach (var e in resLista)
                {
                    Console.WriteLine($"Id: {e.Id} | Caratula: {e.Caratula} | Estado: {e.Estado}");
                }
                Console.WriteLine("--------------------------------------------------");
                break;   
            case "3":
                if (!expedienteIdUlt.HasValue)
                {
                    Console.WriteLine("Primero crea un expediente (Opcion 1)");
                    break;
                }
                Console.WriteLine($"[Id del expediente activo: {expedienteIdUlt}]");
                Console.Write("Ingrese nueva caratula: ");
                var nuevaCaratula = Console.ReadLine()!;
                var reqModificar = new ModificarCaratulaExpedienteRequest(expedienteIdUlt.Value, nuevaCaratula, idUsuarioPrueba);
                var resModificar = modificarCaratulaExpedienteUseCase.Ejecutar(reqModificar);
                Console.WriteLine("Caratula modificada.");
                break;
            case "4":
                if (!expedienteIdUlt.HasValue)
                {
                    Console.WriteLine("Primero crea un expediente (Opcion 1)");
                    break;
                }
                Console.WriteLine($"[Id del expediente activo: {expedienteIdUlt}]");
                Console.WriteLine("\nIngrese un nuevo estado: ");
                Console.WriteLine("   1. Recien iniciado");
                Console.WriteLine("   2. Para resolver");
                Console.WriteLine("   3. Con resolucion");
                Console.WriteLine("   4. En notificacion");
                Console.WriteLine("   5. Finalizado");
                Console.Write("   Opcion: ");
                var nuevoEstado = new EstadoExpediente();
                var op = int.Parse(Console.ReadLine()!);
                switch (op)
                {
                    case 1:
                        nuevoEstado = EstadoExpediente.RecienIniciado;
                        break;
                    case 2:
                        nuevoEstado = EstadoExpediente.ParaResolver;
                        break;
                    case 3:
                        nuevoEstado = EstadoExpediente.ConResolucion;
                        break;
                    case 5:
                        nuevoEstado = EstadoExpediente.EnNotificacion;
                        break;
                    case 6:
                        nuevoEstado = EstadoExpediente.Finalizado;
                        break;
                    default:
                        Console.WriteLine("Estado no valido.");
                        break;
                }
                if(op > 5 && op < 0)
                    nuevoEstado = EstadoExpediente.RecienIniciado;
                var reqCambiarEstado = new CambiarEstadoExpedienteRequest(expedienteIdUlt.Value, nuevoEstado, idUsuarioPrueba);
                var resCambiarEstado = cambiarEstadoExpedienteUseCase.Ejecutar(reqCambiarEstado);
                Console.WriteLine("Estado modificado.");
                break;   
            case "5":
                if (!expedienteIdUlt.HasValue)
                {
                    Console.WriteLine("Primero crea un expediente (Opcion 1)");
                    break;
                }
                Console.Write($"Id del expediente a eliminar [{expedienteIdUlt}]: ");
                eliminarExpedienteUseCase.Ejecutar(new EliminarExpedienteRequest(expedienteIdUlt.Value, idUsuarioPrueba));
                Console.WriteLine("Expediente eliminado correctamente.");
                expedienteIdUlt = null;
                break;
            case "6":

                break;   
            case "7":

                break;   
            case "8":

                break;   
            case "9":

                break;   
            case "0":
                salir=true;
                break;   
            default:
                Console.WriteLine("Opcion no valida.");
                break;
        }
    }
    catch (RepositorioException e)
    {
        Console.WriteLine($"[Error de Repositorio]: {e.Message}");         
    }
    catch (DominioException e)
    {
        Console.WriteLine($"[Error de Dominio]: {e.Message}");
    }
    catch (AutorizacionException e)
    {
        Console.WriteLine($"[Error de permisos]: {e.Message}");
    }
    catch (Exception e)
    {
        Console.WriteLine($"[Error de Sistema]: {e.Message}");
    }

}

Console.ReadKey();