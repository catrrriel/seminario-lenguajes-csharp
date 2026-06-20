using EscuelaApi.Dominio;

namespace EscuelaApi.Aplicacion;

public class RegistrarExamenUseCase(
    IExamenRepository examenRepo,
    IAlumnoRepository alumnoRepo,
    IAutorizacionService autorizacionService,
    IUnidadDeTrabajo uow)
{
        

    public RegistrarExamenResponse Ejecutar(RegistrarExamenRequest request, Guid idUsuario)
    {
        // 1. Control de Autorización: Solo un "Profesor" puede registrar exámenes
        if (!autorizacionService.TieneRol(idUsuario, "Profesor"))
            throw new AutorizacionException("Solo los profesores pueden registrar exámenes.");

        // 2. Validamos alumno (Filtro 404)
        var alumno = alumnoRepo.ObtenerPorId(request.AlumnoId);
        if (alumno == null)
            throw new EntidadNoEncontradaException($"El alumno con ID {request.AlumnoId} no existe.");

        // 3. Regla de Negocio (Filtro 400)
        if (examenRepo.ExisteExamenDeMateria(request.AlumnoId, request.Materia))
            throw new DominioException($"El alumno ya rindió la materia {request.Materia}.");

        // 4. Creamos la entidad
        var nuevoExamen = new Examen(request.AlumnoId, request.Materia, request.Nota, DateTime.Today);
        
        // (Aquí el dominio podría utilizar el idUsuario para auditar quién cargó la nota)

        // 5. Persistencia mediante Unit of Work
        examenRepo.Agregar(nuevoExamen);
        uow.GuardarCambios();

        // 6. Retornamos estrictamente el Response DTO
        return new RegistrarExamenResponse(nuevoExamen.Id);
    }
}