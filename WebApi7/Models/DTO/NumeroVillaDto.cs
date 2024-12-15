using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi7.Models.DTO
{
    public class NumeroVillaDto
    {
        [Required]
        public int NoVilla { get; set; }

        [Required]
        public int VillaId { get; set; }

        [MaxLength(300)]
        public string Detalle { get; set; }

        public VillaDTO Villa { get; set; }
    }
}
