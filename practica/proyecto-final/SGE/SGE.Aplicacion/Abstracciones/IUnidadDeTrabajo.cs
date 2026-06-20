namespace SGE.Aplicacion.Abstracciones;
public interface IUnidadDeTrabajo
{
    void GuardarCambios(); // Confirma de forma atomica los cambios en la base de datos
}