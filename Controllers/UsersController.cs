using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MoviesSeries.Data;
using MoviesSeries.Models;
using MoviesSeries.Models.Dtos;
namespace MoviesSeries.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
      public async Task<int> InsertarUsuario(Usuario usuario)
{
    var nombre = new SqlParameter("@Nombre", usuario.Nombre);
    var apellido = new SqlParameter("@Apellido", usuario.Apellido);
    var nombreUsuario = new SqlParameter("@NombreUsuario", usuario.NombreUsuario);
    var correo = new SqlParameter("@Correo", usuario.Correo);
    var contrasena = new SqlParameter("@Contrasena", usuario.Contrasena);

    var result = await _context.Database.ExecuteSqlRawAsync(
        "EXEC CrearUsuarios @Nombre, @Apellido, @NombreUsuario, @Correo, @Contrasena",
        nombre, apellido, nombreUsuario, correo, contrasena);

    // Nota: Este SP no devuelve el ID del usuario insertado.
    // Si necesitas el ID, deberás modificar el SP para que lo devuelva.
    return result;
}

        [HttpGet("{id}")]
public async Task<ActionResult<UsuarioDto>> GetUser(int id)
{
    var result = await _context.Database
        .SqlQuery<UsuarioDto>($"EXEC ObtenerUsuarioPorID {id}")
        .ToListAsync();

    var usuario = result.FirstOrDefault();

    if (usuario == null)
    {
        return NotFound();
    }

    return usuario;
}
    }
}