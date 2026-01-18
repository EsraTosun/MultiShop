using AutoMapper;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Entites;

namespace MultiShop.Catalog.Mapping
{
    public class FeatureSliderMapping : Profile
    {
        public FeatureSliderMapping()
        {
            CreateMap<FeatureSlider, ResultFeatureSliderDto>();
            CreateMap<FeatureSlider, GetByIdFeatureSliderDto>();

            CreateMap<CreateFeatureSliderDto, FeatureSlider>();
            CreateMap<UpdateFeatureSliderDto, FeatureSlider>();
        }
    }
}
