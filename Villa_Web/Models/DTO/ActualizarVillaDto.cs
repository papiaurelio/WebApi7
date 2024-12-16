using System.ComponentModel.DataAnnotations;

namespace Villa_Web.Models
{
    public class ActualizarVillaDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }

        //[MaxLength(300)]
        public string Detalle { get; set; }
        public int Ocupantes { get; set; }
        public double MetrosCuadrados { get; set; }
        public int Tarifa { get; set; }
        public string ImagenUrl { get; set; }

        //[MaxLength(300)]
        public string Amenidad { get; set; }

    }
}
