using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Usuarios;

namespace SGE.WebApi.TokenProvider;
public class JwtTokenProvider(IConfiguration config) : ITokenProvider
{
    public string GenerarToken(Usuario usuario)
    {
        // Guardamos unicamente el ID del usuario
        var claims = new[] {new Claim("ID", usuario.Id.ToString())};
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Similar a un dto con algunas validaciones
        
        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}