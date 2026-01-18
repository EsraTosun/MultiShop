using AutoMapper;
using MultiShop.Catalog.Dtos.AboutDtos;
using MultiShop.Catalog.Entites;

namespace MultiShop.Catalog.Mapping
{
    public class AboutMapping : Profile
    {
        public AboutMapping()
        {
            CreateMap<About, ResultAboutDto>();
            CreateMap<About, GetByIdAboutDto>();

            CreateMap<CreateAboutDto, About>();
            CreateMap<UpdateAboutDto, About>();
        }
    }
}
