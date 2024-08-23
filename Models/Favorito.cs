using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesSeries.Models
{
    public class Favorito
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioID { get; set; }

        [Required]
        public int MovieID { get; set; }

        public int SerieID { get; set; }

        [ForeignKey("UsuarioID")]
        public required Usuario Usuario { get; set; }

        [ForeignKey("MovieID")]
        public required Movie Movie { get; set; }

        [ForeignKey("SerieID")]
        public required Serie Serie { get; set; }
    }
}