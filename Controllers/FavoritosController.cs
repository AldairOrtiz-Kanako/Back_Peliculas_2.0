using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesSeries.Data;
using MoviesSeries.Models;

namespace MoviesSeries.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoritoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Favorito
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorito>>> GetFavoritos()
        {
            return await _context.Favoritos
                .Include(f => f.User)
                .Include(f => f.Movie)
                .ToListAsync();
        }

        // GET: api/Favorito/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Favorito>> GetFavorito(int id)
        {
            var favorito = await _context.Favoritos
                .Include(f => f.User)
                .Include(f => f.Movie)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (favorito == null)
            {
                return NotFound();
            }

            return favorito;
        }

        // POST: api/Favorito
        [HttpPost]
        public async Task<ActionResult<Favorito>> PostFavorito(Favorito favorito)
        {
            _context.Favoritos.Add(favorito);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFavorito), new { id = favorito.Id }, favorito);
        }

        // DELETE: api/Favorito/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorito(int id)
        {
            var favorito = await _context.Favoritos.FindAsync(id);
            if (favorito == null)
            {
                return NotFound();
            }

            _context.Favoritos.Remove(favorito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Favorito/Usuario/5
        [HttpGet("Usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Favorito>>> GetFavoritosByUsuario(int usuarioId)
        {
            return await _context.Favoritos
                .Where(f => f.UsuarioID == usuarioId)
                .Include(f => f.Movie)
                .ToListAsync();
        }

        private bool FavoritoExists(int id)
        {
            return _context.Favoritos.Any(e => e.Id == id);
        }
    }
}