namespace MoviesSeries.Models
{
    public class Movie
    {
        public string Titulo { get; set; } = string.Empty;
        public DateTime FechaEstreno { get; set; }
        public int? Duracion { get; set; }
        public string Sinopsis { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public string Poster { get; set; } = string.Empty;
        public string Trailer { get; set; } = string.Empty;
    }
}