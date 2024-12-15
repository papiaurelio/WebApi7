using Microsoft.AspNetCore.Mvc.Rendering;

namespace Villa_Web.Models.ViewModel
{
    public class NumeroVillaViewModel
    {
        public NumeroVillaViewModel()
        {
            NumeroVilla = new CrearNumeroVillaDto();
        }

        public CrearNumeroVillaDto NumeroVilla { get; set; }
        public IEnumerable<SelectListItem> VillaList { get; set; } //Seleccion de Villas disponibles.
    }
}
