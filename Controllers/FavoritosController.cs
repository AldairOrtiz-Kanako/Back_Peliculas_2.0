using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesSeries.Data;
using MoviesSeries.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MoviesSeries.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoritosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> AgregarAFavoritos([FromBody] AgregarFavoritoParams parametros)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@UsuarioID", SqlDbType.Int) { Value = parametros.UsuarioID },
                    new SqlParameter("@PeliculasID", SqlDbType.Int) { Value = parametros.PeliculasID.HasValue ? (object)parametros.PeliculasID.Value : DBNull.Value },
                    new SqlParameter("@SeriesID", SqlDbType.Int) { Value = parametros.SeriesID.HasValue ? (object)parametros.SeriesID.Value : DBNull.Value }
                };

                var sql = "EXEC AgregarAFavoritos @UsuarioID, @PeliculasID, @SeriesID";
                
                // Log de los parámetros
                Console.WriteLine($"Executing SP with parameters: UsuarioID={parametros.UsuarioID}, PeliculasID={parametros.PeliculasID}, SeriesID={parametros.SeriesID}");

                await _context.Database.ExecuteSqlRawAsync(sql, parameters);

                return Ok("Agregado a favoritos exitosamente.");
            }
            catch (SqlException ex)
            {
                // Log detallado del error
                Console.WriteLine($"SqlException: {ex.Message}\nError Number: {ex.Number}\nProcedure: {ex.Procedure}");
                return BadRequest($"Error en el procedimiento almacenado: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
                return StatusCode(500, $"Ocurrió un error al agregar a favoritos: {ex.Message}");
            }
        }
    }
}