using AutoMapper;
using MultiShop.Catalog.Dtos.BrandDtos;
using MultiShop.Catalog.Entites;

namespace MultiShop.Catalog.Mapping
{
    public class BrandMapping : Profile
    {
        public BrandMapping()
        {
            CreateMap<Brand, ResultBrandDto>();
            CreateMap<Brand, GetByIdBrandDto>();

            CreateMap<CreateBrandDto, Brand>();
            CreateMap<UpdateBrandDto, Brand>();
        }
    }
}
