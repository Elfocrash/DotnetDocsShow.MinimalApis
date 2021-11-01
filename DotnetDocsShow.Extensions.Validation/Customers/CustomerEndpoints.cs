using FluentValidation;

namespace DotnetDocsShow.Extensions.Validation.Customers;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        app.MapGet("/customers", GetAllCustomers);
        app.MapGet("/customers/{id}", GetCustomerById);
        app.MapPost("/customers", CreateCustomer).WithValidator<Customer>();
        app.MapPut("/customers/{id}", UpdateCustomer);
        app.MapDelete("/customers/{id}", DeleteCustomerById);
    }

    public static void AddCustomerServices(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerService, CustomerService>();
    }

    internal static List<Customer> GetAllCustomers(ICustomerService service)
    {
        return service.GetAll();
    }

    internal static IResult GetCustomerById(ICustomerService service, Guid id)
    {
        var customer = service.GetById(id);
        return customer is not null ? Results.Ok(customer) : Results.NotFound();
    }

    internal static async Task<IResult> CreateCustomerWithValidation(
        ICustomerService service, Customer customer,
        IValidator<Customer> validator)
    {
        var validate = await validator.ValidateAsync(customer);
        if (!validate.IsValid)
        {
            return Results.BadRequest(validate.Errors);
        }

        service.Create(customer);
        return Results.Created($"/customers/{customer.Id}", customer);
    }

    internal static IResult CreateCustomer(
        ICustomerService service, Customer customer)
    {
        service.Create(customer);
        return Results.Created($"/customers/{customer.Id}", customer);
    }

    internal static IResult UpdateCustomer(ICustomerService service, Guid id, Customer updatedCustomer)
    {
        var customer = service.GetById(id);
        if (customer is null)
        {
            return Results.NotFound();
        }

        service.Update(updatedCustomer);
        return Results.Ok(updatedCustomer);
    }

    internal static IResult DeleteCustomerById(ICustomerService service, Guid id)
    {
        service.Delete(id);
        return Results.Ok();
    }
}
