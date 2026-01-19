using FluentValidation;
using MultiShop.Catalog.Dtos.CategoryDtos;

namespace MultiShop.Catalog.Validators.Category
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(c => c.CategoryID)
                .NotEmpty().WithMessage("CategoryId is required.");

            RuleFor(c => c.CategoryName)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category ismi en fazla 100 karakter olabilir.");

            RuleFor(c => c.ImageUrl)
                .MaximumLength(200).WithMessage("ImageURL en fazla 200 karakter olabilir.")
                .Matches(@"^(http(s?)://.*)?$").When(x => !string.IsNullOrEmpty(x.ImageUrl));
        }
    }
}
