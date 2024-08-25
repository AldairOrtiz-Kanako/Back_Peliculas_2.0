using Microsoft.EntityFrameworkCore;
using MoviesSeries.Models;

namespace MoviesSeries.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Genero> Generos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Excluir explícitamente Movie del modelo de EF Core
            modelBuilder.Ignore<Movie>();
            modelBuilder.Ignore<Serie>();
            // Configuraciones adicionales para otras entidades si es necesario
        }
    }
}