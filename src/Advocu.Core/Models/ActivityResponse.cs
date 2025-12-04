using System.Text.Json.Serialization;

namespace Advocu;

// You might define a response model if the API returns structured data upon success
public sealed class ActivityResponse
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    // ... other relevant properties from the API response
}
