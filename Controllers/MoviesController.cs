using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesSeries.Data;
using MoviesSeries.Models;

namespace MoviesSeries.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetCatalogoCompleto()
        {
            try
            {
                var catalogoItems = new List<Movie>();

                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "ObtenerPeliculas";
                        command.CommandType = CommandType.StoredProcedure;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                var item = new Movie
                                {
                                    Titulo = result.GetString(result.GetOrdinal("Titulo")),
                                    FechaEstreno = result.GetDateTime(result.GetOrdinal("FechaEstreno")),
                                    Duracion = result.IsDBNull(result.GetOrdinal("Duracion")) ? null : 
                                    ParseDuration(result.GetValue(result.GetOrdinal("Duracion"))),
                                    Sinopsis = result.GetString(result.GetOrdinal("Sinopsis")),
                                    Genero = result.GetString(result.GetOrdinal("Genero")),
                                    Director = result.GetString(result.GetOrdinal("Director")),
                                    Poster = result.GetString(result.GetOrdinal("Poster")),
                                    Trailer = result.GetString(result.GetOrdinal("Trailer"))
                                };

                                catalogoItems.Add(item);
                            }
                        }
                    }
                }

                return catalogoItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private int? ParseDuration(object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            if (int.TryParse(value.ToString(), out int duration))
                return duration;

            // Si no se puede convertir a int, puedes manejar el error aquí
            // Por ejemplo, podrías registrar un warning y retornar null
            Console.WriteLine($"Warning: No se pudo convertir la duración '{value}' a un número entero.");
            return null;
        }
    }
}