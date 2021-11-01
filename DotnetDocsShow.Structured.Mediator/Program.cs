using DotnetDocsShow.Structured.Mediator;
using DotnetDocsShow.Structured.Mediator.Handlers;
using DotnetDocsShow.Structured.Mediator.Models;
using DotnetDocsShow.Structured.Mediator.Services;
using MediatR;

var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddMediatR(typeof(Customer));

var app = builder.Build();

app.MapGet("customers", async (IMediator mediator) => await mediator.Send(new GetAllCustomersRequest()));
app.MapGet("/customers/{id}", async (IMediator mediator, Guid id) => await mediator.Send(new GetCustomerByIdRequest(id)));
app.MapPost("/customers", async (IMediator mediator, CreateCustomerRequest request) => await mediator.Send(request));
app.MapDelete("/customers/{id}", async (IMediator mediator, Guid id) => await mediator.Send(new DeleteCustomerByIdRequest(id)));

app.Run();
