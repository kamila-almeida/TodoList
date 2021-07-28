using FluentValidation;
using TodoList.Application.Models;

namespace TodoList.Application.Validators
{
    public class UserValidator : AbstractValidator<UserRegisterModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email must be informed.")
                .NotNull().WithMessage("Email must be informed.")
                .EmailAddress().WithMessage("Invalid e-mail format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password must be informed.")
                .NotNull().WithMessage("Password must be informed.")
                .MinimumLength(5).WithMessage("Minimum length required: 5")
                .MaximumLength(32).WithMessage("Maximum length required: 32");
        }
    }
}
