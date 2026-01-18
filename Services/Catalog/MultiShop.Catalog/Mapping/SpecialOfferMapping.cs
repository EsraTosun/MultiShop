using AutoMapper;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Entites;

namespace MultiShop.Catalog.Mapping
{
    public class SpecialOfferMapping : Profile
    {
        public SpecialOfferMapping()
        {
            CreateMap<SpecialOffer, ResultSpecialOfferDto>();
            CreateMap<SpecialOffer, GetByIdSpecialOfferDto>();

            CreateMap<CreateSpecialOfferDto, SpecialOffer>();
            CreateMap<UpdateSpecialOfferDto, SpecialOffer>();
        }
    }
}
