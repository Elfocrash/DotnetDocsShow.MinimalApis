using DotnetDocsShow.Extensions.Validation;
using DotnetDocsShow.Extensions.Validation.Customers;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCustomerServices();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(IAssemblyMarker));

var app = builder.Build();
app.MapCustomerEndpoints();
app.Run();
