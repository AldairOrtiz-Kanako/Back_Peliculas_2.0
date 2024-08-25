using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesSeries.Custom;
using MoviesSeries.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using MoviesSeries.Data;
using System.IdentityModel.Tokens.Jwt;

namespace MoviesSeries.Controllers 
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _dbAppContext;
        private readonly Utilidades _utilidades;
        
        public LoginController(AppDbContext appDbContext, Utilidades utilidades)
        {
            _dbAppContext = appDbContext;
            _utilidades = utilidades;
        }
        
        [HttpPost]
public async Task<IActionResult> Login(LoginDTO objeto)
{
    var usuarioEncontrado = await _dbAppContext.Usuarios
                            .Where(u =>
                                u.Correo == objeto.Correo &&
                                u.Contrasena == objeto.Contrasena
                            ).FirstOrDefaultAsync();
    
    if(usuarioEncontrado == null)
        return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "", userId = 0 });
    else
    {
        var token = _utilidades.generarJWT(usuarioEncontrado);
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        
        var userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "sub");
        var userId = userIdClaim != null ? userIdClaim.Value : usuarioEncontrado.UsuarioID.ToString();

        return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = token, userId = userId });
    }
}
    }
}