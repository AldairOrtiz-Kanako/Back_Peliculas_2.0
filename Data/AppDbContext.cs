//Contexto de la base de datos.


using Microsoft.EntityFrameworkCore;
using MoviesSeries.Models;

namespace MoviesSeries.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
    }
}
