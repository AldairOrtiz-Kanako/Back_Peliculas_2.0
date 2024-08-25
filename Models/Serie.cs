namespace MoviesSeries.Models
{
    public class Serie
    {

        public required string Titulo { get; set; } = string.Empty;
        public required int? Temporadas { get; set; }
        public required int? Episodios { get; set; }
        public required DateTime FechaEstreno { get; set; }
        public required string Sinopsis { get; set; } = string.Empty;
        public required string Genero { get; set; } = string.Empty;
        public required string Director { get; set; } = string.Empty;
        public required string Poster { get; set; } = string.Empty;
        public required string Trailer { get; set; } = string.Empty;
    }
}