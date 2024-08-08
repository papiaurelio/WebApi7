using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Villa_Web.Models
{
    public class NumeroVillaDto
    {
        [Required]
        public int NoVilla { get; set; }

        [Required]
        public int VillaId { get; set; }
        public string Detalle { get; set; }
    }
}
