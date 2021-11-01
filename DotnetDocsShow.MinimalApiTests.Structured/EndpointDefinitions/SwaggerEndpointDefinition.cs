using Microsoft.OpenApi.Models;

namespace DotnetDocsShow.MinimalApiTests.Structured.EndpointDefinitions;

public class SwaggerEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotnetDocsShow.MinimalApiTests.Structured v1"));
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotnetDocsShow.MinimalApiTests.Structured", Version = "v1" });
        });
    }
}
