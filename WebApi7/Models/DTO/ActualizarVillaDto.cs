using System.ComponentModel.DataAnnotations;

namespace WebApi7.Models.DTO
{
    public class ActualizarVillaDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public int Ocupantes { get; set; }
        public double MetrosCuadrados { get; set; }
        public int Tarifa { get; set; }

    }
}
