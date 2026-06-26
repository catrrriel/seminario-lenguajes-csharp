namespace SGE.Aplicacion.Usuarios;
public record class ModificarMisDatosRequest(
    string NuevoNombre, 
    string NuevoEmail, 
    string? NuevaContrasena = null
);
