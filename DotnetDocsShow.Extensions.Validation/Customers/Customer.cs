namespace DotnetDocsShow.Extensions.Validation.Customers;

public class Customer
{
    public Guid Id { get; } = Guid.NewGuid();

    public string FullName { get; init; } = default!;
}
