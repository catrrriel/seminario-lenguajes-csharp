namespace SGE.Dominio.Comun;
public class Entidad
{
    // protected set => para que los hijos puedan asignarle valor (Guid.NewGuid())
    public Guid Id { get; protected set; }
}