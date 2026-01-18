using AutoMapper;
using MultiShop.Catalog.Dtos.OfferDiscountDtos;
using MultiShop.Catalog.Entites;

namespace MultiShop.Catalog.Mapping
{
    public class OfferDiscountMapping : Profile
    {
        public OfferDiscountMapping()
        {
            CreateMap<OfferDiscount, ResultOfferDiscountDto>();
            CreateMap<OfferDiscount, GetByIdOfferDiscountDto>();

            CreateMap<CreateOfferDiscountDto, OfferDiscount>();
            CreateMap<UpdateOfferDiscountDto, OfferDiscount>();
        }
    }
}
