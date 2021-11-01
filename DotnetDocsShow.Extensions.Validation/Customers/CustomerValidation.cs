using FluentValidation;

namespace DotnetDocsShow.Extensions.Validation.Customers;

public class CustomerValidation : AbstractValidator<Customer>
{
    public CustomerValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FullName).NotEmpty();
    }
}
