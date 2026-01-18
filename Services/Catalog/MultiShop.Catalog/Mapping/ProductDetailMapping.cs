using AutoMapper;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Entites;

namespace MultiShop.Catalog.Mapping
{
    public class ProductDetailMapping : Profile
    {
        public ProductDetailMapping()
        {
            CreateMap<ProductDetail, ResultProductDetailDto>();
            CreateMap<ProductDetail, GetByIdProductDetailDto>();

            CreateMap<CreateProductDetailDto, ProductDetail>();
            CreateMap<UpdateProductDetailDto, ProductDetail>();
        }
    }
}
