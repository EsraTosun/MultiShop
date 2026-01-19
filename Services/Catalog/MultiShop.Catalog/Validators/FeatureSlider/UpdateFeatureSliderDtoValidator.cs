using FluentValidation;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;

namespace MultiShop.Catalog.Validators.FeatureSlider
{
    public class UpdateFeatureSliderDtoValidator : AbstractValidator<UpdateFeatureSliderDto>
    {
        public UpdateFeatureSliderDtoValidator()
        {
            RuleFor(x => x.FeatureSliderId)
                .NotEmpty().WithMessage("FeatureSliderId alanı zorunludur.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık alanı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Başlık en az 3 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama alanı boş bırakılamaz.")
                .MinimumLength(10).WithMessage("Açıklama en az 10 karakter olmalıdır.")
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Görsel URL alanı boş bırakılamaz.");
        }
    }
}
