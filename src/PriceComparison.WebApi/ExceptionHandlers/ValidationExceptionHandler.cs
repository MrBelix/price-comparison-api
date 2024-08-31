using System.Collections.Immutable;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PriceComparison.WebApi.ExceptionHandlers;

public class ValidationExceptionHandler : IExceptionHandler
{
    private const int StatusCode = StatusCodes.Status403Forbidden;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        // If not validation exception just exit
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        var errors = validationException.Errors.GroupBy(x => x.PropertyName)
            .ToImmutableDictionary(x => x.Key, x => x.Select(y => y.ErrorMessage).ToImmutableList());

        var problemDetails = new ProblemDetails
        {
            Title = "One or more validation errors occurred",
            Detail = validationException.Message,
            Status = StatusCode,
            Extensions =
            {
                { nameof(errors),  errors }
            }
        };

        httpContext.Response.StatusCode = StatusCode;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}