using FluentValidation;
using MultiShop.Catalog.Dtos.AboutDtos;

namespace MultiShop.Catalog.Validators.About
{
    public class UpdateAboutDtoValidator : AbstractValidator<UpdateAboutDto>
    {
        public UpdateAboutDtoValidator()
        {
            RuleFor(a => a.AboutId)
                .NotEmpty().WithMessage("AboutId is required.");

            RuleFor(a => a.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(a => a.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(250).WithMessage("Adres en fazla 250 karakter olabilir.");

            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress();

            RuleFor(a => a.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?\d{10,15}$");
        }
    }
}
