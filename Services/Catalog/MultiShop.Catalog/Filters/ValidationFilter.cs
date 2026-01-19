using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var errors = new Dictionary<string, string[]>();

        foreach (var param in context.ActionArguments)
        {
            if (param.Value == null) continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(param.Value.GetType());
            var validator = context.HttpContext.RequestServices.GetService(validatorType);

            if (validator == null) continue;

            var validationContext =
                new ValidationContext<object>(param.Value);

            var result = await ((IValidator)validator)
                .ValidateAsync(validationContext);

            if (!result.IsValid)
            {
                errors[param.Key] = result.Errors
                    .Select(e => e.ErrorMessage)
                    .ToArray();
            }
        }

        if (errors.Any())
        {
            var problemDetails = new ValidationProblemDetails(errors)
            {
                Title = "Validation Error",
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(problemDetails);
            return;
        }

        await next();
    }
}
