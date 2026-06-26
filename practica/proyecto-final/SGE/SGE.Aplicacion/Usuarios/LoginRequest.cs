namespace SGE.Aplicacion.Usuarios;
public record class LoginRequest(
    string Email, 
    string Contrasena
);
