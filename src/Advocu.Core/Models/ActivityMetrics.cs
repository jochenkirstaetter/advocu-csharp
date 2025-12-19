using System.ComponentModel.DataAnnotations; // For data annotations if needed for validation
using System.Text.Json.Serialization;

namespace Advocu;

/// <summary>
/// Represents various metrics associated with an activity.
/// </summary>
public sealed class ActivityMetrics
{
    /// <summary>
    /// Gets or sets the number of readers or views.
    /// </summary>
    [Range(1, int.MaxValue)] // Minimum 1 for metricsReaders
    [JsonPropertyName("readers")]
    public int? Readers { get; set; } // Nullable int as it might be optional based on context, but min 1 if provided

    /// <summary>
    /// Gets or sets the number of attendees.
    /// </summary>
    [Range(1, int.MaxValue)] // minimum 1
    [JsonPropertyName("attendees")]
    public int? Attendees { get; set; } // "How many people have been trained?"
    
    /// <summary>
    /// Gets or sets the time spent in minutes.
    /// </summary>
    [Range(1, int.MaxValue)] // minimum 1
    [JsonPropertyName("timeSpent")]
    public int? TimesSpent { get; set; } // "Time spent (in minutes)"

    /// <summary>
    /// Gets or sets the impact of the activity.
    /// </summary>
    [Range(1, int.MaxValue)] // minimum 1
    [JsonPropertyName("impact")]
    public int? Impact { get; set; } // "What was your impact in views, reads, attendees, etc.? Add a number below:"
}