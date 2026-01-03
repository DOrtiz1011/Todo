using FluentValidation;
using Todo.Api.DTO;

namespace Todo.Api.Validators
{
    public class TodoTaskRequestDtoValidator : AbstractValidator<TodoTaskRequestDTO>
    {
        public TodoTaskRequestDtoValidator()
        {
            RuleFor(x => x.title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.description)
                .MaximumLength(500).WithMessage("Description is too long.");
            
            RuleFor(x => x.duedatetime)
                .GreaterThan(DateTime.Now).WithMessage("Due date must be in the future.");
        }
    }
}
