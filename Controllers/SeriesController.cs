using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesSeries.Data;
using MoviesSeries.Models;

namespace MoviesSeries.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Serie>>> GetSeries()
        {
            return await _context.Series
                .Include(s => s.Genero)
                .Include(s => s.Director)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Serie>> GetSerie(int id)
        {
            var serie = await _context.Series
                .Include(s => s.Genero)
                .Include(s => s.Director)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (serie == null)
            {
                return NotFound();
            }

            return serie;
        }

    }
}