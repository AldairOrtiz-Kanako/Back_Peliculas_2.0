using System.ComponentModel.DataAnnotations;

namespace MoviesSeries.Models
{
    public class AgregarFavoritoParams
    {
        [Required]
        public int UsuarioID { get; set; }

        public int? PeliculasID { get; set; }

        public int? SeriesID { get; set; }
    }
}