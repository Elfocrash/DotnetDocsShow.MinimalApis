using FluentValidation;

namespace DotnetDocsShow.Extensions.Validation;

public static class ValidationExtensions
{
    public static RouteHandlerBuilder WithValidator<TType>(
        this RouteHandlerBuilder builder) where TType : class
    {
        builder.Add(endpointBuilder =>
        {
            var originalRequestDelegate = endpointBuilder.RequestDelegate;
            endpointBuilder.RequestDelegate = async context =>
            {
                var validator = context.RequestServices.GetService<IValidator<TType>>();

                if (validator is null)
                {
                    await originalRequestDelegate!(context);
                    return;
                }

                context.Request.EnableBuffering();
                var model = await context.Request.ReadFromJsonAsync<TType>();
                if (model is null)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        error = "Couldn't map the model from the request body"
                    });
                    return;
                }

                var result = await validator.ValidateAsync(model);
                if (!result.IsValid)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsJsonAsync(new { errors = result.Errors });
                    return;
                }

                context.Request.Body.Position = 0;
                await originalRequestDelegate!(context);
            };
        });
        return builder;
    }
}
