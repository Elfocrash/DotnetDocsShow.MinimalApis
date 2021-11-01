using System.Text.Json.Serialization;
using DotnetDocsShow.Structured.Mediator.Models;
using DotnetDocsShow.Structured.Mediator.Services;
using MediatR;

namespace DotnetDocsShow.Structured.Mediator.Handlers;

public record CreateCustomerRequest : IRequest<IResult>
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; } = Guid.NewGuid();

    [JsonPropertyName("fullName")]
    public string FullName { get; init; } = default!;
}

public class CreateCustomerRequestHandler
    : IRequestHandler<CreateCustomerRequest, IResult>
{
    private readonly ICustomerService _customerService;

    public CreateCustomerRequestHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public Task<IResult> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            FullName = request.FullName,
            Id = request.Id
        };
        _customerService.Create(customer);
        var response = Results.Created($"/customers/{customer.Id}", customer);
        return Task.FromResult(response);
    }
}
