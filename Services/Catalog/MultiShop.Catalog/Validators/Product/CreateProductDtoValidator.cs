using FluentValidation;
using MultiShop.Catalog.Dtos.ProductDtos;

namespace MultiShop.Catalog.Validators.Product
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Ürün adı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Ürün adı en az 3 karakter olmalıdır.")
                .MaximumLength(150).WithMessage("Ürün adı en fazla 150 karakter olabilir.");

            RuleFor(x => x.ProductPrice)
                .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");

            RuleFor(x => x.ProductImageUrl)
                .NotEmpty().WithMessage("Ürün ana görseli boş bırakılamaz.");

            RuleFor(x => x.ProductDescription)
                .NotEmpty().WithMessage("Ürün açıklaması boş bırakılamaz.")
                .MaximumLength(1000).WithMessage("Ürün açıklaması en fazla 1000 karakter olabilir.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Kategori seçimi zorunludur.");
        }
    }
}
