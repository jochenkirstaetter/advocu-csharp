using System.Text.Json.Serialization;

namespace Advocu;

// You might define a response model if the API returns structured data upon success
public class CreateContentCreationActivityResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    // ... other relevant properties from the API response
}
