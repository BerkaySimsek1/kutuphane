using FluentValidation;
using KutuphaneTakip.Models;

namespace KutuphaneTakip.Validators
{
    public class LoanValidator : AbstractValidator<Loan>
    {
        public LoanValidator()
        {
            RuleFor(loan => loan.MemberId)
                .NotEmpty().WithMessage("MemberId is required.");

            RuleFor(loan => loan.BookId)
                .NotEmpty().WithMessage("BookId is required.");

            RuleFor(loan => loan.LoanDate)
                .NotEmpty().WithMessage("Loan date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Loan date cannot be in the future.");

            RuleFor(loan => loan.ReturnDate)
                .GreaterThan(loan => loan.LoanDate).WithMessage("Return date must be after the loan date.")
                .When(loan => loan.ReturnDate.HasValue).WithMessage("Return date is optional but must be valid if provided.");
        }
    }
}