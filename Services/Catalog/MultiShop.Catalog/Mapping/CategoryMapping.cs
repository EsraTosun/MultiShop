using AutoMapper;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entites;

namespace MultiShop.Catalog.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, ResultCategoryDto>();
            CreateMap<Category, GetByIdCategoryDto>();

            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
