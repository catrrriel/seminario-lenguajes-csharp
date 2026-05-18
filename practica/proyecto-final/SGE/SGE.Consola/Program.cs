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
    if (expedienteIdUlt.HasValue)
        Console.WriteLine($"\n[Expediente activo: {expedienteIdUlt}]");
    Console.Write("\nOpcion: ");
    var opcion = Console.ReadLine();

    try
    {
        switch (opcion)
        {
            case "1":
                Console.Write("Ingrese caratula del expediente a agregar: ");
                string caratulaExp = Console.ReadLine() ?? "Sin caratula";
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
                Console.WriteLine($"Id del expediente activo: [{expedienteIdUlt}]");
                Console.Write("Ingrese nueva caratula: ");
                var nuevaCaratula = Console.ReadLine() ?? "Sin caratula";
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
                Console.WriteLine($"\n[Id del expediente activo: {expedienteIdUlt}]");
                Console.WriteLine("\nIngrese un nuevo estado: ");
                Console.WriteLine("   1. Recien iniciado");
                Console.WriteLine("   2. Para resolver");
                Console.WriteLine("   3. Con resolucion");
                Console.WriteLine("   4. En notificacion");
                Console.WriteLine("   5. Finalizado");
                Console.Write("  Opcion: ");

                var nuevoEstado = new EstadoExpediente();
                int opEstado = int.Parse(Console.ReadLine() ?? "1");
                nuevoEstado = opEstado switch
                {
                    1 => EstadoExpediente.RecienIniciado,
                    2 => EstadoExpediente.ParaResolver,
                    3 => EstadoExpediente.ConResolucion,
                    4 => EstadoExpediente.EnNotificacion,
                    5 => EstadoExpediente.Finalizado,
                    _ => EstadoExpediente.RecienIniciado
                };
                
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
                Console.WriteLine($"Id del expediente a eliminar [{expedienteIdUlt}]");
                var reqEliminarExpediente = new EliminarExpedienteRequest(expedienteIdUlt.Value, idUsuarioPrueba);
                eliminarExpedienteUseCase.Ejecutar(reqEliminarExpediente);
                Console.WriteLine("Expediente eliminado correctamente.");
                expedienteIdUlt = null;
                break;
            case "6":
                if (!expedienteIdUlt.HasValue)
                    {
                        Console.WriteLine("Primero crea un expediente (Opcion 1)");
                        break;
                    }
                    Console.WriteLine($"Agregando tramite al expediente: [{expedienteIdUlt}]");
                    Console.Write("Ingrese contenido del tramite: ");
                    var contenidoTramite = Console.ReadLine() ?? "Sin contenido";

                    // para simplificar la prueba 
                    var etiquetaNueva = EtiquetaTramite.PaseAEstudio; 

                    var reqAgregarTramite = new AgregarTramiteRequest(expedienteIdUlt.Value, etiquetaNueva, contenidoTramite, idUsuarioPrueba);
                    var resAgregarTramite = agregarTramiteUseCase.Ejecutar(reqAgregarTramite);

                    tramiteIdUlt = resAgregarTramite.Id;

                    Console.WriteLine($"Tramite agregado con Id: [{resAgregarTramite.Id}]");
                    break;
            case "7":
                if (!tramiteIdUlt.HasValue)
                {
                    Console.WriteLine("Primero crea un tramite (Opcion 6) para poder eliminarlo");
                    break;
                }
                Console.WriteLine($"Id del tramite a eliminar [{tramiteIdUlt}]");
                var reqEliminarTramite = new EliminarTramiteRequest(tramiteIdUlt.Value, idUsuarioPrueba);
                eliminarTramiteUseCase.Ejecutar(reqEliminarTramite);

                Console.WriteLine("Tramite eliminado correctamente.");
                tramiteIdUlt = null;
                break;   
            case "8":
                if (!tramiteIdUlt.HasValue)
                {
                    Console.WriteLine("Primero crea un tramite (Opcion 6) para poder modificarlo.");
                    break;
                }

                Console.WriteLine($"Id del trámite activo: [{tramiteIdUlt}]");
                Console.Write("Ingrese el nuevo contenido del tramite: ");
                var nuevoContenido = Console.ReadLine() ?? "Sin contenido";

                Console.WriteLine("\nSeleccione la nueva etiqueta:");
                Console.WriteLine(" 1. Escrito Presentado");
                Console.WriteLine(" 2. Pase a Estudio");
                Console.WriteLine(" 3. Despacho");
                Console.WriteLine(" 4. Resolucion");
                Console.WriteLine(" 5. Notificacion");
                Console.WriteLine(" 6. Pase al Archivo");
                Console.Write(" Opcion: ");

                var nuevaEtiqueta = new EtiquetaTramite();
                int opEtiqueta = int.Parse(Console.ReadLine() ?? "1");
                nuevaEtiqueta = opEtiqueta switch
                {
                    1 => EtiquetaTramite.EscritoPresentado,
                    2 => EtiquetaTramite.PaseAEstudio,
                    3 => EtiquetaTramite.Despacho,
                    4 => EtiquetaTramite.Resolucion,
                    5 => EtiquetaTramite.Notificacion,
                    6 => EtiquetaTramite.PaseAlArchivo,
                    _ => EtiquetaTramite.EscritoPresentado
                };

                var reqModificarTramite = new ModificarTramiteRequest(tramiteIdUlt.Value, nuevaEtiqueta, nuevoContenido, idUsuarioPrueba);
                modificarTramiteUseCase.Ejecutar(reqModificarTramite);

                Console.WriteLine("Tramite modificado");
                break; 
            case "9":
                if (!expedienteIdUlt.HasValue)
                    {
                        Console.WriteLine("Primero crea un expediente (Opcion 1)");
                        break;
                    }
                    Console.WriteLine($"------------- Tramites del Exp: [{expedienteIdUlt}] -------------");
                    var reqListarTramites = new ListarTramitesPorExpedienteRequest(expedienteIdUlt.Value);
                    var resListaTramites = listarTramitesPorExpediente.Ejecutar(reqListarTramites);

                    foreach (var t in resListaTramites)
                    {
                        Console.WriteLine($"Id: {t.Id} | Etiqueta: {t.Etiqueta} | Contenido: {t.Contenido}");
                    }
                    Console.WriteLine("--------------------------------------------------");
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