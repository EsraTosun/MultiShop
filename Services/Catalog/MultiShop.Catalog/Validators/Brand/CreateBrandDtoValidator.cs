using FluentValidation;
using MultiShop.Catalog.Dtos.AboutDtos;
using MultiShop.Catalog.Dtos.BrandDtos;

namespace MultiShop.Catalog.Validators.Brand
{
    public class CreateBrandDtoValidator : AbstractValidator<CreateBrandDto>
    {
        public CreateBrandDtoValidator()
        {
            RuleFor(a => a.BrandName)
                .NotEmpty().WithMessage("BrandName is required.");

            RuleFor(a => a.ImageUrl)
                .NotEmpty().WithMessage("ImageUrl is required.")
                .MaximumLength(500).WithMessage("ImageUrl en fazla 500 karakter olabilir.");
        }
    }
}
