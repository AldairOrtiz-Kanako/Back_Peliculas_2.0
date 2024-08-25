using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesSeries.Custom;
using MoviesSeries.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using MoviesSeries.Data;

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
                return StatusCode(StatusCodes.Status200OK, new{isSuccess = false, toke = ""});
            else
                return StatusCode(StatusCodes.Status200OK, new{isSuccess = true, toke = _utilidades.generarJWT(usuarioEncontrado)});
        }
    }
}