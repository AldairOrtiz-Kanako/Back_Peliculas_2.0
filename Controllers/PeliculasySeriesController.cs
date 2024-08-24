using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesSeries.Data;
using MoviesSeries.Models;

namespace MoviesSeries.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculasySeriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeliculasySeriesController(AppDbContext context)
        {
            _context = context;
        }

[HttpGet]
public async Task<ActionResult<IEnumerable<CatalogoItem>>> GetCatalogoCompleto()
{
    try
    {
        var catalogoItems = new List<CatalogoItem>();

        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "ObtenerCatalogoCompleto";
            command.CommandType = CommandType.StoredProcedure;

            await _context.Database.OpenConnectionAsync();

            using (var result = await command.ExecuteReaderAsync())
            {
                while (await result.ReadAsync())
                {
                    var item = new CatalogoItem
                    {
                        Tipo = result.GetString(result.GetOrdinal("Tipo")),
                        Titulo = result.GetString(result.GetOrdinal("Titulo")),
                        FechaEstreno = result.GetDateTime(result.GetOrdinal("FechaEstreno")),
                        Sinopsis = result.GetString(result.GetOrdinal("Sinopsis")),
                        Genero = result.GetString(result.GetOrdinal("Genero")),
                        Director = result.GetString(result.GetOrdinal("Director")),
                        Poster = result.GetString(result.GetOrdinal("Poster")),
                        Trailer = result.GetString(result.GetOrdinal("Trailer")),
                        Temporadas = result.IsDBNull(result.GetOrdinal("Temporadas")) ? null : 
                        int.TryParse(result.GetValue(result.GetOrdinal("Temporadas")).ToString(), out int temp) ? (int?)temp : null,
                        Episodios = result.IsDBNull(result.GetOrdinal("Episodios")) ? null : 
                        int.TryParse(result.GetValue(result.GetOrdinal("Episodios")).ToString(), out int ep) ? (int?)ep : null,
                        Duracion = result.IsDBNull(result.GetOrdinal("Duracion")) ? null : 
                        int.TryParse(result.GetValue(result.GetOrdinal("Duracion")).ToString(), out int dur) ? (int?)dur : null
                    };

                    catalogoItems.Add(item);
                }
            }
        }

        return catalogoItems;
    }
    catch (Exception ex)
{
    // Log the full exception details
    Console.WriteLine($"Error: {ex}");
    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
}
}
}

}
