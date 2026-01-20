using FluentValidation;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Validators
{
    public class UpdateDiscountCouponDtoValidator : AbstractValidator<UpdateDiscountCouponDto>
    {
        public UpdateDiscountCouponDtoValidator()
        {
            RuleFor(x => x.CouponId)
                .GreaterThan(0).WithMessage("Geçerli bir kupon ID girilmelidir.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Kupon kodu boş olamaz.")
                .MaximumLength(20).WithMessage("Kupon kodu en fazla 20 karakter olabilir.");

            RuleFor(x => x.Rate)
                .InclusiveBetween(1, 100)
                .WithMessage("Kupon indirimi 1 ile 100 arasında olmalıdır.");

            RuleFor(x => x.ValidDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("Kupon geçerlilik tarihi bugünden sonra olmalıdır.");
        }
    }
}
