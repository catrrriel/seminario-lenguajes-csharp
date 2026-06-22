using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Comun;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class ActualizacionEstadoExpedienteService(IExpedienteRepository expedienteRepositorio, ITramiteRepository tramiteRepositorio)
{
    private readonly IExpedienteRepository _expedienteRepositorio = expedienteRepositorio;
    private readonly ITramiteRepository _tramiteRepositorio = tramiteRepositorio;

    public void Ejecutar (Guid expedienteId, Guid idUsuario)
    {
        var expediente = _expedienteRepositorio.ObtenerPorId(expedienteId)
            ?? throw new EntidadNoEncontradaException("No se encontro el expediente asociado al tramite");
        
        var tramites = _tramiteRepositorio.ObtenerPorExpedienteId(expediente.Id);

        EtiquetaTramite? ultimaEtiqueta = null;
        DateTime? fechaMasReciente = null;
        foreach (var t in tramites)
        {
            if(fechaMasReciente == null || t.FechaCreacion > fechaMasReciente)
            {
                fechaMasReciente = t.FechaCreacion;
                ultimaEtiqueta = t.Etiqueta;
            }
        }

        expediente.ActualizarEstado(ultimaEtiqueta, idUsuario);
    }
}