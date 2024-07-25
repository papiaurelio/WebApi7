using System.ComponentModel.DataAnnotations;

namespace WebApi7.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }

        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
    }
}
