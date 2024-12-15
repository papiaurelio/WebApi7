using Microsoft.AspNetCore.Mvc.Rendering;

namespace Villa_Web.Models.ViewModel
{
    public class NumeroVilllaActualizarViewModel
    {
        public NumeroVilllaActualizarViewModel()
        {
            NumeroVilla = new ActualizarNumeroVillaDto();
        }

        public ActualizarNumeroVillaDto NumeroVilla { get; set; }
        public IEnumerable<SelectListItem> VillaList { get; set; } //Seleccion de Villas disponibles.
    }
}
