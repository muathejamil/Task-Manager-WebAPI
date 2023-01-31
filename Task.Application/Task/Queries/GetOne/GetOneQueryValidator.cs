using FluentValidation;

namespace Task.Application.Task.Queries.GetOne;

public class GetOneQueryValidator : AbstractValidator<GetOneQuery>
{
    public GetOneQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty();
    }
    
}