using EscuelaApi.Dominio;

namespace EscuelaApi.Aplicacion;

public class AgregarAlumnoUseCase(
    IAutorizacionService autorizacionService,
    IAlumnoRepository alumnoRepo,
    IUnidadDeTrabajo uow)
{
    public AgregarAlumnoResponse Ejecutar(AgregarAlumnoRequest request, Guid idUsuario)
    {
        // 1. Control de Autorización estricto
        if (!autorizacionService.TieneRol(idUsuario, "Administrador"))
            throw new AutorizacionException("Solo un Administrador puede dar de alta alumnos.");
        
        // 2. Validamos y mapeamos el string del DTO al Value Object del Dominio
        var partesEmail = request.Email.Split('@');
        if (partesEmail.Length != 2)
        {
            // Lanzamos DominioException para que nuestro futuro filtro devuelva un HTTP 400
            throw new DominioException("El formato del email no es válido.");
        }

        var direccionEmail = new DireccionEmail(partesEmail[0], partesEmail[1]);

        // 3. Instanciamos la Entidad (el Guid se genera automáticamente dentro del constructor)
        var nuevoAlumno = new Alumno(request.Nombre, direccionEmail);

        // 4. Persistencia
        alumnoRepo.Agregar(nuevoAlumno);
        uow.GuardarCambios();

        return new AgregarAlumnoResponse(nuevoAlumno.Id);
    }
}