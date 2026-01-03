using FluentValidation;
using Todo.Api.DTO;

namespace Todo.Api.Validators
{
    public class TodoTaskRequestDtoValidator : AbstractValidator<TodoTaskRequestDTO>
    {
        public TodoTaskRequestDtoValidator()
        {
            RuleFor(x => x.title)
                .NotNull().WithMessage("Title is required.")
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.description)
                .MaximumLength(500).WithMessage("Description is too long.");
            
            RuleFor(x => x.duedatetime)
                .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.")
                .When(x => x.duedatetime.HasValue);
        }
    }
}
