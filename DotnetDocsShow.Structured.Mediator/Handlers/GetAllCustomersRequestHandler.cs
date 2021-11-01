using DotnetDocsShow.Structured.Mediator.Services;
using MediatR;

namespace DotnetDocsShow.Structured.Mediator.Handlers;

public record GetAllCustomersRequest : IRequest<IResult>;

public class GetAllCustomersRequestHandler
    : IRequestHandler<GetAllCustomersRequest, IResult>
{
    private readonly ICustomerService _customerService;

    public GetAllCustomersRequestHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public Task<IResult> Handle(
        GetAllCustomersRequest request, CancellationToken cancellationToken)
    {
        var customers = _customerService.GetAll();
        var response = Results.Ok(customers);
        return Task.FromResult(response);
    }
}
