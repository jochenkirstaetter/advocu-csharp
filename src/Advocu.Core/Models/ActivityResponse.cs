using System.Text.Json.Serialization;

namespace Advocu;

// You might define a response model if the API returns structured data upon success
/// <summary>
/// Represents the response received after making an activity request.
/// </summary>
public sealed class ActivityResponse
{
    /// <summary>
    /// Gets or sets the ID of the activity.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the status of the activity.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    // ... other relevant properties from the API response
}
