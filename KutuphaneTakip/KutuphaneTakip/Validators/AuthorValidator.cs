using FluentValidation;
using KutuphaneTakip.Models;

namespace KutuphaneTakip.Validators
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(author => author.Name)
                .NotEmpty().WithMessage("Author name is required.")
                .Length(2, 50).WithMessage("Author name length must be between 2 and 50 characters.");

            RuleFor(author => author.Surname)
                .NotEmpty().WithMessage("Author surname is required.")
                .Length(2, 50).WithMessage("Author surname length must be between 2 and 50 characters.");
        }
    }
}