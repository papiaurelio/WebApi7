using WebApi7.Models.DTO;

namespace WebApi7.Datos
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
        {
            new VillaDTO{Id=1, Nombre="Casa Byron", Ocupantes = 5, MetrosCuadrados= 100},
            new VillaDTO{Id=2, Nombre="Casa Casa Casa",  Ocupantes = 3, MetrosCuadrados= 10},
            new VillaDTO{Id=3, Nombre="Casa 2 2 2",  Ocupantes = 10, MetrosCuadrados= 2000}
        };
    }
}
