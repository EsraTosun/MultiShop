using AutoMapper;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entites;

namespace MultiShop.Catalog.Mapping
{
    public class ProductImageMapping : Profile
    {
        public ProductImageMapping()
        {
            CreateMap<ProductImage, ResultProductImageDto>();
            CreateMap<ProductImage, GetByIdProductImageDto>();

            CreateMap<CreateProductImageDto, ProductImage>();
            CreateMap<UpdateProductImageDto, ProductImage>();
        }
    }
}
