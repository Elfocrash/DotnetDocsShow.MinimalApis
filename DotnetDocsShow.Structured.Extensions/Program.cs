using DotnetDocsShow.Structured.Extensions.Customers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomerServices();

var app = builder.Build();

app.MapCustomerEndpoints();

app.Run();
