using EscuelaApi.Dominio;

namespace EscuelaApi.Aplicacion;

public class ConsultarUsuariosUseCase(IUsuarioRepository usuarioRepo)
{
    public ConsultarUsuariosResponse Ejecutar(ConsultarUsuariosRequest request)
    {
        // 1. Obtenemos las entidades vivas
        var usuarios = usuarioRepo.ObtenerTodos();
        
        // 2. Mapeamos hacia los DTOs de salida
        var usuariosDto = usuarios.Select(u => 
            new UsuarioDto(
                u.Id, 
                u.Nombre, 
                u.Email.ToString(), 
                u.Rol));
            
        // 3. Empaquetamos y retornamos
        return new ConsultarUsuariosResponse(usuariosDto);
    }
}