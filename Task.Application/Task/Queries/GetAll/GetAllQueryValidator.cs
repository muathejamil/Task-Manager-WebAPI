using FluentValidation;

namespace Task.Application.Task.Queries.GetAll;

public class GetAllQueryValidator : AbstractValidator<GetAllQuery>
{
    public GetAllQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0);
    }
    
}