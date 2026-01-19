using FluentValidation;
using MultiShop.Catalog.Dtos.ContactDtos;

namespace MultiShop.Catalog.Validators.Contact
{
    public class UpdateContactDtoValidator : AbstractValidator<UpdateContactDto>
    {
        public UpdateContactDtoValidator()
        {
            RuleFor(x => x.ContactId)
                .NotEmpty().WithMessage("ContactId boş olamaz.");

            RuleFor(x => x.NameSurname)
                .NotEmpty().WithMessage("Ad Soyad boş olamaz.")
                .MinimumLength(3).WithMessage("Ad Soyad en az 3 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Ad Soyad en fazla 100 karakter olabilir.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Konu boş olamaz.")
                .MaximumLength(150).WithMessage("Konu en fazla 150 karakter olabilir.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Mesaj boş olamaz.")
                .MinimumLength(10).WithMessage("Mesaj en az 10 karakter olmalıdır.")
                .MaximumLength(1000).WithMessage("Mesaj en fazla 1000 karakter olabilir.");
        }
    }
}
