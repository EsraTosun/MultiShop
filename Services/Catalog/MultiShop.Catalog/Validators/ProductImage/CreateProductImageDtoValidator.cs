using FluentValidation;
using MultiShop.Catalog.Dtos.ProductImageDtos;

namespace MultiShop.Catalog.Validators.ProductImage
{
    public class CreateProductImageDtoValidator : AbstractValidator<CreateProductImageDto>
    {
        public CreateProductImageDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId alanı zorunludur.");

            RuleFor(x => x)
                .Must(x =>
                    !string.IsNullOrEmpty(x.Image1) ||
                    !string.IsNullOrEmpty(x.Image2) ||
                    !string.IsNullOrEmpty(x.Image3) ||
                    !string.IsNullOrEmpty(x.Image4))
                .WithMessage("En az bir adet ürün görseli eklenmelidir.");
        }
    }
}
