using FluentValidation;

namespace Task.Application.Task.Commands.Import;

public class ImportCommandValidator : AbstractValidator<ImportCommand>
{
    public ImportCommandValidator()
    {
        RuleFor(x => x.Tasks)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.Tasks.Where(t => t.Title == null || t.Title == string.Empty))
            .Must(x => !x.Any())
            .WithMessage("Title is required");
        
        RuleFor(x => x.Tasks.Where(t => t.StartDate == null || t.StartDate == DateTime.MinValue || t.StartDate >= t.EndDate))
            .Must(x => !x.Any())
            .WithMessage("StartDate is required");
        
        RuleFor(x => x.Tasks.Where(t => t.EndDate == null || t.EndDate == DateTime.MinValue || t.EndDate <= t.StartDate))
            .Must(x => !x.Any())
            .WithMessage("EndDate is required");
    }
    
}