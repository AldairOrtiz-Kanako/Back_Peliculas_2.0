using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MoviesSeries.Models;

namespace MoviesSeries.Custom{
    public class Utilidades
    {
       private readonly IConfiguration _configuration;
       public Utilidades(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Generación de token
        public String generarJWT(Usuario modelo)
        {
            //Crear la información del usuario para token
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, modelo.UsuarioID.ToString()),
                new Claim(ClaimTypes.Email, modelo.Correo!)
            };

            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //Crear detalle del token
            var JWTConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(JWTConfig);

        }


    }
}