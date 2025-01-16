using FluentValidation;
using KutuphaneTakip.Models;

namespace KutuphaneTakip.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(2, 100).WithMessage("Title length must be between 2 and 100 characters.");

            RuleFor(book => book.AuthorId)
                .GreaterThan(0).WithMessage("AuthorId must be a positive integer.");

            RuleFor(book => book.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be a positive integer.");

            RuleFor(book => book.Metadata)
                .NotEmpty().WithMessage("Metadata is required.");
        }
    }

}
