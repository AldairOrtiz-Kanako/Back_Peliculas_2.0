
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesSeries.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Duracion { get; set; } // Duration in minutes
        public required string Descripcion { get; set; }
        public required string Image { get; set; }

        [ForeignKey("Director")]
        public int DirectorId { get; set; }
        public required Director Director { get; set; }

        [ForeignKey("Genero")]
        public int GeneroId { get; set; }
        public required Genero Genero { get; set; }
    }
}