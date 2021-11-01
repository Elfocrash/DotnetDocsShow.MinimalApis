namespace DotnetDocsShow.MinimalApiTests.Structured;

public interface IEndpointDefinition
{
    void DefineServices(IServiceCollection services);

    void DefineEndpoints(WebApplication app);
}
