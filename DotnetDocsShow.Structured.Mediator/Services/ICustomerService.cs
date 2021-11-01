using DotnetDocsShow.Structured.Mediator.Models;

namespace DotnetDocsShow.Structured.Mediator.Services;

public interface ICustomerService
{
    void Create(Customer? customer);

    Customer? GetById(Guid id);

    List<Customer> GetAll();

    void Update(Customer customer);

    void Delete(Guid id);
}
