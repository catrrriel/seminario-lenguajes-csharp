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
bool salir = false;
while (!salir)
{
    Console.WriteLine("\n=== SGE - Sistema de Gestion de Expedientes ===");
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
                Console.WriteLine("Agregando expediente");
                var reqAgregar = new AgregarExpedienteRequest($"Expediente 001", idUsuarioPrueba);
                var resAgregar = agregarExpedienteUseCase.Ejecutar(reqAgregar);
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
                Console.WriteLine("Modificando Caratula");
                var expedienteId = Guid.Parse("cd5d2a83-ce75-4a6d-b9da-858a7cb5f67d");
                var nuevaCaratula = "Expediente 002";
                var reqModificar = new ModificarCaratulaExpedienteRequest(expedienteId, nuevaCaratula, idUsuarioPrueba);
                var resModificar = modificarCaratulaExpedienteUseCase.Ejecutar(reqModificar);
                Console.WriteLine("Caratula modificada.");
                break;   
            case "4":
                Console.WriteLine("Cambiando estado.");
                expedienteId = Guid.Parse("cd5d2a83-ce75-4a6d-b9da-858a7cb5f67d");
                var nuevoEstado = EstadoExpediente.ParaResolver;
                var reqCambiarEstado = new CambiarEstadoExpedienteRequest(expedienteId, nuevoEstado, idUsuarioPrueba);
                var resCambiarEstado = cambiarEstadoExpedienteUseCase.Ejecutar(reqCambiarEstado);
                Console.WriteLine("Estado modificado.");
                break;   
            case "5":
                Console.WriteLine("Intentando borrar expediente");

                //var reqEliminar = new EliminarExpedienteRequest();

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