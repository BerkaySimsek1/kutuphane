using FluentValidation;
using KutuphaneTakip.Models;

namespace KutuphaneTakip.Validators
{
    public class MemberValidator : AbstractValidator<Member>
    {
        public MemberValidator()
        {
            RuleFor(member => member.Name)
                .NotEmpty().WithMessage("Member name is required.")
                .Length(2, 50).WithMessage("Member name length must be between 2 and 50 characters.");

            RuleFor(member => member.Surname)
                .NotEmpty().WithMessage("Member surname is required.")
                .Length(2, 50).WithMessage("Member surname length must be between 2 and 50 characters.");

            RuleFor(member => member.MembershipDate)
                .NotEmpty().WithMessage("Membership date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Membership date cannot be in the future.");
        }
    }
}