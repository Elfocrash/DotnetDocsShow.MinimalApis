using DotnetDocsShow.Structured.Scanning;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointDefinitions(typeof(IEndpointDefinition));

var app = builder.Build();
app.UseEndpointDefinitions();

app.Run();
