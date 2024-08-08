using AutoMapper;
using Villa_Web.Models;

namespace Villa_Web.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDTO, CrearVillaDto>().ReverseMap();
            CreateMap<VillaDTO, ActualizarVillaDto>().ReverseMap();

            CreateMap<NumeroVillaDto, CrearNumeroVillaDto>().ReverseMap();
            CreateMap<NumeroVillaDto, ActualizarNumeroVillaDto>().ReverseMap();

        }
    }
}
