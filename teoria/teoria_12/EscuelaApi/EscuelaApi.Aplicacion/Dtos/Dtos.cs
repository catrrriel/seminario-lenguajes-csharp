namespace EscuelaApi.Aplicacion;

// --------------------------------------------------------
// DTOs Compartidos
// --------------------------------------------------------
public record class ExamenDto(Guid Id, string Materia, double Nota, DateTime Fecha);
public record class AlumnoDto(Guid Id, string Nombre, string Email);
public record class UsuarioDto(Guid Id, string Nombre, string Email, string Rol);



// --------------------------------------------------------
// DTOs para AgregarAlumnoUseCase
// --------------------------------------------------------
public record class AgregarAlumnoRequest(string Nombre, string Email);

public record class AgregarAlumnoResponse(Guid Id);



// --------------------------------------------------------
// DTOs para ConsultarExamenesAlumnoUseCase
// --------------------------------------------------------
public record class ConsultarExamenesAlumnoRequest(Guid AlumnoId);

public record class ConsultarExamenesAlumnoResponse(IEnumerable<ExamenDto> Examenes);



// --------------------------------------------------------
// DTOs para RegistrarExamenUseCase
// --------------------------------------------------------
public record class RegistrarExamenRequest(Guid AlumnoId, string Materia, double Nota);

public record class RegistrarExamenResponse(Guid Id);



// --------------------------------------------------------
// DTOs para EliminarAlumnoUseCase
// --------------------------------------------------------
public record class EliminarAlumnoRequest(Guid AlumnoId);

// Podemos devolver un registro vacío o el ID del alumno eliminado como confirmación
public record class EliminarAlumnoResponse(Guid IdEliminado);



// --------------------------------------------------------
// DTOs para ModificarNotaDeExamenUseCase
// --------------------------------------------------------
public record class ModificarNotaRequest(Guid ExamenId, double NuevaNota);

public record class ModificarNotaResponse(Guid Id);



// --------------------------------------------------------
// DTOs para ConsultarAlumnosUseCase
// --------------------------------------------------------
public record class ConsultarAlumnosResponse(IEnumerable<AlumnoDto> Alumnos);

// Creamos el record vacío para mantener el contrato
public record class ConsultarAlumnosRequest(); 


// --------------------------------------------------------
// DTOs para ConsultarUsuariosUseCase
// --------------------------------------------------------
public record class ConsultarUsuariosRequest();

public record class ConsultarUsuariosResponse(IEnumerable<UsuarioDto> Usuarios);