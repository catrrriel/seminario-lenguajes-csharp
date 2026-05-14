using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class AgregarTramiteUseCase(ITramiteRepository repositorio, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService actualizacionEstado)
{
    private readonly ITramiteRepository _repositorio = repositorio;
    private readonly IAutorizacionService _autorizacion = autorizacion;
    private readonly ActualizacionEstadoExpedienteService _actualizacionEstado = actualizacionEstado;

    public AgregarTramiteResponse Ejecutar (AgregarTramiteRequest request)
    {
        if(!_autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.TramiteAlta))
            throw new AutorizacionException("El usuario no tiene permiso para agregar tramites");
        
        var contenido = new ContenidoTramite(request.Contenido);
        var tramite = new Tramite(request.ExpedienteID, request.Etiqueta, contenido, request.IdUsuario);

        _repositorio.Agregar(tramite);

        return new AgregarTramiteResponse(tramite.Id, tramite.Etiqueta, tramite.Contenido.Valor);
    }
}