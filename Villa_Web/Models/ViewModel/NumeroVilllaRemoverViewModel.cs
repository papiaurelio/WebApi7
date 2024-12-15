using Microsoft.AspNetCore.Mvc.Rendering;

namespace Villa_Web.Models.ViewModel
{
    public class NumeroVilllaRemoverViewModel
    {
        public NumeroVilllaRemoverViewModel()
        {
            NumeroVilla = new NumeroVillaDto();
        }

        public NumeroVillaDto NumeroVilla { get; set; }
        public IEnumerable<SelectListItem> VillaList { get; set; } //Seleccion de Villas disponibles.
    }
}
