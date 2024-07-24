using WebApi7.Models.DTO;

namespace WebApi7.Datos
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
        {
            new VillaDTO{Id=1, Nombre="Casa Byron"},
            new VillaDTO{Id=2, Nombre="Casa Casa Casa"},
            new VillaDTO{Id=3, Nombre="Casa 2 2 2"}
        };
    }
}
