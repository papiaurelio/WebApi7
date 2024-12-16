using System.ComponentModel.DataAnnotations;

namespace WebApi7.Models.DTO
{
    public class CrearNumeroVillaDto
    {
        [Required]
        public int NoVilla { get; set; }

        [Required]
        public int VillaId { get; set; }

        [MaxLength(300, ErrorMessage = "El texto es muy largo.")]
        public string Detalle { get; set; }
    }
}
