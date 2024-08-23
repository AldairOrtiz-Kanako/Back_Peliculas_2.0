//Se crean las clases que utilizaremos.

namespace MoviesSeries.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string NombreUsuario { get; set; }
        public required string Correo { get; set; }
        public required string Contrasena { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }

}
