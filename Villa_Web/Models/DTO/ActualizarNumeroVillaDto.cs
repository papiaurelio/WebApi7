using System.ComponentModel.DataAnnotations;

namespace Villa_Web.Models
{
    public class ActualizarNumeroVillaDto
    {
        [Required]
        public int NoVilla { get; set; }

        [Required]
        public int VillaId { get; set; }

        [MaxLength(300)]
        public string Detalle { get; set; }
    }
}
