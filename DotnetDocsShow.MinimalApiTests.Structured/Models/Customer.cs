using System.Text.Json.Serialization;

namespace DotnetDocsShow.MinimalApiTests.Structured.Models;

public class Customer
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; } = Guid.NewGuid();

    [JsonPropertyName("fullName")]
    public string FullName { get; init; } = default!;
}
