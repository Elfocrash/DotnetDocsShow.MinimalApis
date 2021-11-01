using System.Text.Json.Serialization;
using DotnetDocsShow.Structured.Mediator.Services;
using MediatR;

namespace DotnetDocsShow.Structured.Mediator.Handlers;

public record DeleteCustomerByIdRequest : IRequest<IResult>
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    public DeleteCustomerByIdRequest(Guid id)
    {
        Id = id;
    }
}

public class DeleteCustomerByIdRequestHandler
    : IRequestHandler<DeleteCustomerByIdRequest, IResult>
{
    private readonly ICustomerService _customerService;

    public DeleteCustomerByIdRequestHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public Task<IResult> Handle(DeleteCustomerByIdRequest request, CancellationToken cancellationToken)
    {
        _customerService.Delete(request.Id);
        return Task.FromResult(Results.Ok());
    }
}
