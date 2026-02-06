using AutoMapper;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entites;

namespace MultiShop.Catalog.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            // CREATE / UPDATE
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // GET
            CreateMap<Product, ResultProductDto>();
            CreateMap<Product, GetByIdProductDto>();

            CreateMap<Product, ResultProductsWithCategoryDto>();
        }
    }
}
