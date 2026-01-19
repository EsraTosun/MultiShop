using FluentValidation;
using MultiShop.Catalog.Dtos.FeatureDtos;

namespace MultiShop.Catalog.Validators.Feature
{
    public class UpdateFeatureDtoValidator : AbstractValidator<UpdateFeatureDto>
    {
        public UpdateFeatureDtoValidator()
        {
            RuleFor(x => x.FeatureId)
                .NotEmpty().WithMessage("FeatureId boş olamaz.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz.")
                .MinimumLength(3).WithMessage("Başlık en az 3 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage("Icon boş olamaz.")
                .MaximumLength(100).WithMessage("Icon en fazla 100 karakter olabilir.");
        }
    }
}
