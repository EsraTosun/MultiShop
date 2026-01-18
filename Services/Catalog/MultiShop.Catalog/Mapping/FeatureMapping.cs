using AutoMapper;
using MultiShop.Catalog.Dtos.FeatureDtos;
using MultiShop.Catalog.Entites;

namespace MultiShop.Catalog.Mapping
{
    public class FeatureMapping : Profile
    {
        public FeatureMapping()
        {
            CreateMap<Feature, ResultFeatureDto>();
            CreateMap<Feature, GetByIdFeatureDto>();

            CreateMap<CreateFeatureDto, Feature>();
            CreateMap<UpdateFeatureDto, Feature>();
        }
    }
}
