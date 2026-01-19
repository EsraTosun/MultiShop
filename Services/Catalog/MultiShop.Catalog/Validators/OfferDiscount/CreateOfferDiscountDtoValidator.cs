using FluentValidation;
using MultiShop.Catalog.Dtos.OfferDiscountDtos;

namespace MultiShop.Catalog.Validators.OfferDiscount
{
    public class CreateOfferDiscountDtoValidator : AbstractValidator<CreateOfferDiscountDto>
    {
        public CreateOfferDiscountDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık alanı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Başlık en az 3 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

            RuleFor(x => x.SubTitle)
                .NotEmpty().WithMessage("Alt başlık alanı boş bırakılamaz.")
                .MaximumLength(150).WithMessage("Alt başlık en fazla 150 karakter olabilir.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Görsel URL alanı boş bırakılamaz.");

            RuleFor(x => x.ButtonTitle)
                .NotEmpty().WithMessage("Buton başlığı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Buton başlığı en fazla 50 karakter olabilir.");
        }
    }
}
