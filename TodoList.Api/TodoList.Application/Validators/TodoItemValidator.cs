using FluentValidation;
using TodoList.Application.Models;

namespace TodoList.Application.Validators
{
    public class TodoItemValidator : AbstractValidator<TodoItemRegisterModel>
    {
        public TodoItemValidator()
        {       
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description must be informed.")
                .NotNull().WithMessage("Description must be informed.")
                .MinimumLength(5).WithMessage("Minimum length required: 5")
                .MaximumLength(300).WithMessage("Maximum length required: 300");

            RuleFor(x => x.DueDate)
                .NotEmpty().WithMessage("Due date must be informed.")
                .NotNull().WithMessage("Due date must be informed.");
        }
    }
}
