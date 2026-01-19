using FluentValidation;
using MultiShop.Catalog.Dtos.CategoryDtos;

namespace MultiShop.Catalog.Validators.Category
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(c => c.CategoryName)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category ismi en fazla 100 karakter olabilir.");

            RuleFor(c => c.ImageUrl)
                .MaximumLength(200).WithMessage("ImageURL en fazla 200 karakter olabilir.")
                .Matches(@"^(http(s?)://.*)?$").When(x => !string.IsNullOrEmpty(x.ImageUrl));
        }
    }
}
