using FluentValidation;
using KutuphaneTakip.Models;

namespace KutuphaneTakip.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .Length(2, 50).WithMessage("Category name length must be between 2 and 50 characters.");
        }
    }
}