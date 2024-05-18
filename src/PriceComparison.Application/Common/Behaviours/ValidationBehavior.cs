using System.Collections.Immutable;
using FluentValidation;
using MediatR;

namespace PriceComparison.Application.Common.Behaviours;

public sealed class ValidationBehavior<TRequest, TResponse>(
    IValidator<TRequest> validator) : IPipelineBehavior<TRequest, TResponse>
where TRequest : IBaseRequest
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var response = await next();

        return response;
    }
}