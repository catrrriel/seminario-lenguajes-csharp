namespace EscuelaApi.Dominio;

public class Examen : Entidad
{
    public Guid AlumnoId { get; private set; }
    public string Materia { get; private set; } = "";
    public double Nota { get; private set; }
    public DateTime Fecha { get; private set; }
    public Examen(Guid alumnoId, string materia, double nota, DateTime fecha)
    {
        // completar aquí las validaciones que aseguren la consistencia de la entidad
        AlumnoId = alumnoId;
        Materia = materia;
        Nota = nota;
        Fecha = fecha;
    }

    protected Examen() { } // Constructor para EF Core

    public void ModificarNota(double nota)
    {
        if (nota < 0 || nota > 10)
        {
            throw new DominioException("Valor para la nota no permitido");
        }
        Nota = nota;
    }

    // Otros métodos que implementan el comportamiento de la entidad 
    // manteniendo sus invariantes

}
