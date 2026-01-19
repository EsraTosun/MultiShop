using FluentValidation;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;

namespace MultiShop.Catalog.Validators.SpecialOffer
{
    public class CreateSpecialOfferDtoValidator : AbstractValidator<CreateSpecialOfferDto>
    {
        public CreateSpecialOfferDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık alanı boş bırakılamaz.")
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(x => x.SubTitle)
                .NotEmpty().WithMessage("Alt başlık alanı boş bırakılamaz.")
                .MaximumLength(150);

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Görsel URL alanı boş bırakılamaz.");
        }
    }
}
