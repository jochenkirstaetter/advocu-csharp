using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Advocu;

public abstract class ActivityRequest
{
    [Required]
    [MinLength(3)]
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    [MaxLength(2000)]
    [JsonPropertyName("description")]
    public required string Description { get; set; } // Max length 2000 as per image

    [JsonPropertyName("tags")]
    [MinLength(0)] // min items 0
    public List<string> Tags { get; set; } = new List<string>();

    [MaxLength(2000)]
    [JsonPropertyName("additionalInfo")]
    public string? AdditionalInfo { get; set; }

    [JsonPropertyName("private")]
    public bool? Private { get; set; }

    [JsonPropertyName("metrics")]
    public ActivityMetrics Metrics { get; set; } = new();
}