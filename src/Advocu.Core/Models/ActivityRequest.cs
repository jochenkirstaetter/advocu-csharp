using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Advocu;

/// <summary>
/// Represents the base request for creating an activity.
/// </summary>
public abstract class ActivityRequest
{
    /// <summary>
    /// Gets or sets the title of the activity.
    /// </summary>
    [Required]
    [MinLength(3)]
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the activity.
    /// </summary>
    [MaxLength(2000)]
    [JsonPropertyName("description")]
    public required string Description { get; set; } // Max length 2000 as per image

    /// <summary>
    /// Gets or sets the list of tags associated with the activity.
    /// </summary>
    [JsonPropertyName("tags")]
    [MinLength(0)] // min items 0
    public List<AdvocuTag> Tags { get; set; } = [];

    /// <summary>
    /// Gets or sets additional information about the activity.
    /// </summary>
    [MaxLength(2000)]
    [JsonPropertyName("additionalInfo")]
    public string? AdditionalInfo { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the activity is private.
    /// </summary>
    [JsonPropertyName("private")]
    public bool? Private { get; set; }

    /// <summary>
    /// Gets or sets the metrics associated with the activity.
    /// </summary>
    [JsonPropertyName("metrics")]
    public ActivityMetrics Metrics { get; set; } = new();
}