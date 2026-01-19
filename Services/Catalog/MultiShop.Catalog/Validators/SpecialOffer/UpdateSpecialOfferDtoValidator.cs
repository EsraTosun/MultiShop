using FluentValidation;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;

namespace MultiShop.Catalog.Validators.SpecialOffer
{
    public class UpdateSpecialOfferDtoValidator : AbstractValidator<UpdateSpecialOfferDto>
    {
        public UpdateSpecialOfferDtoValidator()
        {
            RuleFor(x => x.SpecialOfferId)
                .NotEmpty().WithMessage("SpecialOfferId alanı zorunludur.");

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
