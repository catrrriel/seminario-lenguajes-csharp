using EscuelaApi.Dominio;

namespace EscuelaApi.Aplicacion;


public class EliminarAlumnoUseCase(
    IAutorizacionService autorizacionService,
    IAlumnoRepository alumnoRepo,
    IExamenRepository examenRepo,
    IUnidadDeTrabajo uow)
{
    public EliminarAlumnoResponse Ejecutar(EliminarAlumnoRequest request, Guid idUsuario)
    {
        // 1. Control de Autorización estricto
        if (!autorizacionService.TieneRol(idUsuario, "Administrador"))
            throw new AutorizacionException("Solo un Administrador puede dar de alta alumnos.");
        
        // 2. Validamos que el alumno exista
        var alumno = alumnoRepo.ObtenerPorId(request.AlumnoId);
        if (alumno == null)
            throw new EntidadNoEncontradaException($"El alumno con ID {request.AlumnoId} no existe.");

        // 3. Buscamos todos los exámenes asociados
        var examenes = examenRepo.ObtenerPorAlumno(request.AlumnoId);

        // 4. Orquestación: Eliminamos primero los exámenes (Dependencias)
        foreach (var examen in examenes)
        {
            examenRepo.Eliminar(examen.Id);
        }

        // 5. Orquestación: Eliminamos al alumno (Entidad Principal)
        alumnoRepo.Eliminar(request.AlumnoId);

        // 6. ¡LA MAGIA DEL UNIT OF WORK!
        uow.GuardarCambios();

        return new EliminarAlumnoResponse(request.AlumnoId);
    }
}