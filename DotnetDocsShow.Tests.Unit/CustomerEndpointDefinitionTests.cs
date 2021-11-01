using System;
using System.Collections.Generic;
using System.Linq;
using DotnetDocsShow.MinimalApiTests.Structured.EndpointDefinitions;
using DotnetDocsShow.MinimalApiTests.Structured.Models;
using DotnetDocsShow.MinimalApiTests.Structured.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DotnetDocsShow.Tests.Unit;

public class CustomerEndpointDefinitionTests
{
    private readonly ICustomerService _customerService =
        Substitute.For<ICustomerService>();

    private readonly CustomerEndpointDefinition _sut = new();

    [Fact]
    public void GetAllCustomers_ReturnEmptyList_WhenNoCustomersExist()
    {
        //Arrange
        _customerService.GetAll().Returns(new List<Customer>());

        //Act
        var result = _sut.GetAllCustomers(_customerService);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void GetAllCustomers_ReturnsCustomer_WhenCustomerExists()
    {
        //Arrange
        var id = Guid.NewGuid();
        var customer = new Customer { Id = id, FullName = "Nick Chapsas" };
        _customerService.GetAll().Returns(new List<Customer> { customer });

        //Act
        var result = _sut.GetAllCustomers(_customerService);

        //Assert
        result.Should().ContainSingle(x => x.Id == id && x.FullName == "Nick Chapsas");
    }
}
