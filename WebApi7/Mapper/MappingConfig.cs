using AutoMapper;
using WebApi7.Models;
using WebApi7.Models.DTO;

namespace WebApi7.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();

            CreateMap<Villa, CrearVillaDto>().ReverseMap();
            CreateMap<Villa, ActualizarVillaDto>().ReverseMap();

            CreateMap<NumeroVilla, NumeroVillaDto>().ReverseMap();
            CreateMap<NumeroVilla, CrearNumeroVillaDto>().ReverseMap();
            CreateMap<NumeroVilla, ActualizarNumeroVillaDto>().ReverseMap();

        }
    }
}
