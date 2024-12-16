using System.ComponentModel.DataAnnotations;

namespace Villa_Web.Models
{
    public class CrearVillaDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(150)]
        public string Nombre { get; set; }

        //[MaxLength(300)]
        public string Detalle { get; set; }
        public int Ocupantes { get; set; }
        public double MetrosCuadrados { get; set; }

        [Required(ErrorMessage = "La tarifa es requerida")]
        public int Tarifa { get; set; }
        public string ImagenUrl { get; set; }

        //[MaxLength(300)]
        public string Amenidad { get; set; }

    }
}
