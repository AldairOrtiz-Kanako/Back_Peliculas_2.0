namespace MoviesSeries.Models
{
public class CatalogoItem
{
    public required string Tipo { get; set; }
    public required string Titulo { get; set; }
    public int? Temporadas { get; set; }
    public int? Episodios { get; set; }
    public required DateTime FechaEstreno { get; set; }
    public required string Sinopsis { get; set; }
    public required string Genero { get; set; }
    public required string Director { get; set; }
    public required string Poster { get; set; }
    public required string Trailer { get; set; }
    public int? Duracion { get; set; }
}

}