using FluentValidation;
using MediatR;
using Task.Application.Common.CustomExceptions.User;

namespace Task.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest>? _validator;
    public ValidationBehavior(IValidator<TRequest>? validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {

        // Validate the  command/query here
        if (_validator is not null)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                //TODO: We could handle the error response in a better way
                var firstOrDefault = validationResult.Errors
                    .Select(x => x.ErrorMessage)
                    .FirstOrDefault();
                throw new BadRequestException(firstOrDefault!);
            }
        }

        // Continue the pipeline
        var result = await next();
        return result;

    }
}