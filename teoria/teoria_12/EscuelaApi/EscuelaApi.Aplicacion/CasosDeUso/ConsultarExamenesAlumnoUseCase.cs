using EscuelaApi.Dominio;

namespace EscuelaApi.Aplicacion;

public class ConsultarExamenesAlumnoUseCase(IExamenRepository examenRepo, IAlumnoRepository alumnoRepo)
{   
    public ConsultarExamenesAlumnoResponse Ejecutar(ConsultarExamenesAlumnoRequest request)
    {
        // 1. Validamos que el alumno exista (para el Filtro 404)
        var alumno = alumnoRepo.ObtenerPorId(request.AlumnoId);
        if (alumno == null)
            throw new EntidadNoEncontradaException($"El alumno con ID {request.AlumnoId} no existe.");

        // 2. Buscamos los exámenes
        var examenes = examenRepo.ObtenerPorAlumno(request.AlumnoId);

        // 3. Mapeamos la entidad al DTO compartido
        var examenesDto = examenes.Select(e => new ExamenDto(e.Id, e.Materia, e.Nota, e.Fecha));

        // 4. Retornamos estrictamente el Response DTO
        return new ConsultarExamenesAlumnoResponse(examenesDto);
    }
}