using System.ComponentModel.DataAnnotations;

namespace WebApi7.Models.DTO
{
    public class CrearVillaDto
    {
        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }


        [MaxLength(300)]
        public string Detalle { get; set; }
        public int Ocupantes { get; set; }
        public double MetrosCuadrados { get; set; }

        [Required]
        public int Tarifa { get; set; }
        public string ImagenUrl { get; set; }

        [MaxLength(300)]
        public string Amenidad { get; set; }

    }
}
