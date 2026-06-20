using EscuelaApi.Dominio;

namespace EscuelaApi.Aplicacion;

public class ConsultarAlumnosUseCase(IAlumnoRepository alumnoRepo)
{
    // Por el momento recibe el request vacío por parámetro, quizá el día de mañana 
    // si necesitamos paginar en este parámetro recibirá datos relevantes
    public ConsultarAlumnosResponse Ejecutar(ConsultarAlumnosRequest request)
    {
        var alumnos = alumnoRepo.ObtenerTodos();
        
        var alumnosDto = alumnos.Select(a => 
            new AlumnoDto(a.Id, a.Nombre, a.Email?.ToString() ?? ""));
            
        return new ConsultarAlumnosResponse(alumnosDto);
    }
}