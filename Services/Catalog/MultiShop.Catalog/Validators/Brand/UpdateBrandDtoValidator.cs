using FluentValidation;
using MultiShop.Catalog.Dtos.BrandDtos;

namespace MultiShop.Catalog.Validators.Brand
{
    public class UpdateBrandDtoValidator : AbstractValidator<UpdateBrandDto>
    {
        public UpdateBrandDtoValidator()
        {
            RuleFor(a => a.BrandId)
                .NotEmpty().WithMessage("BrandId is required.");

            RuleFor(a => a.BrandName)
                .NotEmpty().WithMessage("BrandName is required.");

            RuleFor(a => a.ImageUrl)
                .NotEmpty().WithMessage("ImageUrl is required.")
                .MaximumLength(500).WithMessage("ImageUrl en fazla 500 karakter olabilir.");
        }
    }
}
