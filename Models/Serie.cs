
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesSeries.Models
{
    public class Serie
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public required int Temporada { get; set; }
        public required int Episodios { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public required string Descripcion { get; set; }
        public required string Image { get; set; }

        [ForeignKey("Genero")]
        public int GeneroId { get; set; }
        public required Genero Genero { get; set; }

        [ForeignKey("Director")]
        public int DirectorId { get; set; }
        public required Director Director { get; set; }
    }
}