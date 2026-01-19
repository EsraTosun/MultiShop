using FluentValidation;
using MultiShop.Catalog.Dtos.ProductDetailDtos;

namespace MultiShop.Catalog.Validators.ProductDetail
{
    public class CreateProductDetailDtoValidator : AbstractValidator<CreateProductDetailDto>
    {
        public CreateProductDetailDtoValidator()
        {
            RuleFor(x => x.ProductDescription)
                .NotEmpty().WithMessage("Ürün açıklaması boş bırakılamaz.")
                .MinimumLength(10).WithMessage("Ürün açıklaması en az 10 karakter olmalıdır.")
                .MaximumLength(1000).WithMessage("Ürün açıklaması en fazla 1000 karakter olabilir.");

            RuleFor(x => x.ProductInfo)
                .NotEmpty().WithMessage("Ürün bilgisi boş bırakılamaz.")
                .MaximumLength(1000).WithMessage("Ürün bilgisi en fazla 1000 karakter olabilir.");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId alanı zorunludur.");
        }
    }
}
