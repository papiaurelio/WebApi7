namespace WebApi7.Models
{
    public class Villa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
    }
}
