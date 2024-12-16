using System.ComponentModel.DataAnnotations;

namespace WebApi7.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }

        [MaxLength(300, ErrorMessage = "El texto es muy largo.")]
        public string Detalle { get; set; }
        public int Ocupantes { get; set; }
        public double MetrosCuadrados { get; set; }
        public int Tarifa { get; set; }
        public string ImagenUrl { get; set; }

        [MaxLength(300, ErrorMessage = "El texto es muy largo.")]
        public string Amenidad { get; set; }

    }
}
