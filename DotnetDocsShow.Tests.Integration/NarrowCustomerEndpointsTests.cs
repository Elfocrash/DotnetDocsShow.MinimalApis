using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using DotnetDocsShow.MinimalApiTests.Structured.Models;
using DotnetDocsShow.MinimalApiTests.Structured.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace DotnetDocsShow.Tests.Integration;

public class NarrowCustomerEndpointsTests
{
    private readonly ICustomerService _customerService =
        Substitute.For<ICustomerService>();

    [Fact]
    public async Task GetCustomerById_ReturnCustomer_WhenCustomerExists()
    {
        //Arrange
        var id = Guid.NewGuid();
        var customer = new Customer{ Id = id, FullName = "Nick Chapsas"};
        _customerService.GetById(Arg.Is(id)).Returns(customer);

        using var app = new TestApplicationFactory(x =>
        {
            x.AddSingleton(_customerService);
        });

        var httpClient = app.CreateClient();

        //Act
        var response = await httpClient.GetAsync($"/customers/{id}");
        var responseText = await response.Content.ReadAsStringAsync();
        var customerResult = JsonSerializer.Deserialize<Customer>(responseText);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        customerResult.Should().BeEquivalentTo(customer);
    }

    [Fact]
    public async Task GetCustomerById_ReturnNotFound_WhenCustomerDoesNotExists()
    {
        //Arrange
        _customerService.GetById(Arg.Any<Guid>()).Returns((Customer?)null);

        using var app = new TestApplicationFactory(x =>
        {
            x.AddSingleton(_customerService);
        });

        var guid = Guid.NewGuid();
        var httpClient = app.CreateClient();

        //Act
        var response = await httpClient.GetAsync($"/customers/{guid}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
