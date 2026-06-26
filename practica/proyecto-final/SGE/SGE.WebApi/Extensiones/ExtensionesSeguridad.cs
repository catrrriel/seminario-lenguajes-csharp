using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SGE.Aplicacion.Autorizacion;
using SGE.WebApi.TokenProvider;

namespace SGE.WebApi.Extensiones;
public static class ExtensionesSeguridad
{
    public static IServiceCollection AddAutorizacionJWT(this IServiceCollection servicios, IConfiguration config)
    {
        servicios.AddScoped<ITokenProvider, JwtTokenProvider>();
        servicios.AddAuthorization();
        // Configuramos el validador oficial de Microsoft
        servicios.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opciones =>
        {
            opciones.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,  // Validar quien lo emitio
                ValidateAudience = true, // Validar para quien es
                ValidateLifetime = true, // Validar que no este vencido
                ValidateIssuerSigningKey = true, // Validar la firma criptografica

                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
            };
        });

        return servicios; // Permite un encadenamiento, devolviendo el propio objeto 
    }
}