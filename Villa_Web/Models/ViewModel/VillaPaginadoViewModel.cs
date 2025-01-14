namespace Villa_Web.Models.ViewModel
{
    public class VillaPaginadoViewModel
    {
        public int PagesNumber { get; set; }

        public int TotalPages { get; set; }

        public string Previo { get; set; } = "disabled";  //Para trabajar con la botones 

        public string Siguiente { get; set; } = "";  //Para trabajar con la botones 

        public IEnumerable<VillaDTO> VillaList { get; set; }

    }
}
