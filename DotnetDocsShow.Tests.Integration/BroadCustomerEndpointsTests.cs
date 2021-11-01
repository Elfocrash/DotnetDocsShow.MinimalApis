using System;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using DotnetDocsShow.MinimalApiTests.Structured.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace DotnetDocsShow.Tests.Integration;

public class BroadCustomerEndpointsTests
{
    [Fact]
    public async Task GetCustomerById_ReturnCustomer_WhenCustomerExists()
    {
        //Arrange
        var id = Guid.NewGuid();
        var customer = new Customer{ Id = id, FullName = "Nick Chapsas"};

        using var app = new TestApplicationFactory();

        var httpClient = app.CreateClient();
        await httpClient.PostAsJsonAsync("/customers", customer);

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
        using var app = new TestApplicationFactory();

        var guid = Guid.NewGuid();
        var httpClient = app.CreateClient();

        //Act
        var response = await httpClient.GetAsync($"/customers/{guid}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
