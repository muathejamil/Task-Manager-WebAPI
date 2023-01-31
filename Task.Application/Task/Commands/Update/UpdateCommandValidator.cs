using FluentValidation;

namespace Task.Application.Task.Commands.Update;

public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
{
    public UpdateCommandValidator()
    {
        RuleFor(x => x.Task)
            .NotNull();
        RuleFor(x => x.Task.Title)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.Task.StartDate)
            .NotNull()
            .NotEmpty()
            .LessThan(x => x.Task.EndDate);

        RuleFor(x => x.Task.EndDate)
            .NotNull()
            .NotEmpty()
            .GreaterThan(x => x.Task.StartDate)
            .GreaterThanOrEqualTo(x => DateTime.Now);
    }
    
}