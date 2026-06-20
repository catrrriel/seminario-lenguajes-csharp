using EscuelaApi.Dominio;

namespace EscuelaApi.Aplicacion;

public class ModificarNotaDeExamenUseCase(
    IExamenRepository examenRepo, 
    IAutorizacionService authService, 
    IUnidadDeTrabajo uow)
{
    public ModificarNotaResponse Ejecutar(ModificarNotaRequest request, Guid idUsuario)
    {
        // 1. Control de Autorización (Escenario Protegido)
        if (!authService.TieneRol(idUsuario, "Profesor"))
            throw new AutorizacionException("Solo los profesores pueden modificar las notas de los exámenes.");

        // 2. Buscamos la entidad REAL y "viva" en la base de datos
        var examen = examenRepo.ObtenerPorId(request.ExamenId);
        if (examen == null)
            throw new EntidadNoEncontradaException($"El examen con ID {request.ExamenId} no existe.");

        // 3. Ejecutamos el comportamiento del dominio (se validan las reglas)
        examen.ModificarNota(request.NuevaNota);

        // 4. ¡La magia de Entity Framework Core!
        // El Change Tracker sabe que la propiedad 'Nota' de este examen específico mutó.
        // Al guardar, genera un UPDATE solo para esa columna.
        uow.GuardarCambios();

        return new ModificarNotaResponse(examen.Id);
    }
}