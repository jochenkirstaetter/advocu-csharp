using System.ComponentModel.DataAnnotations; // For data annotations if needed for validation
using System.Text.Json.Serialization;

namespace Advocu;

public sealed class ActivityMetrics
{
    [Range(1, int.MaxValue)] // Minimum 1 for metricsReaders
    [JsonPropertyName("readers")]
    public int? Readers { get; set; } // Nullable int as it might be optional based on context, but min 1 if provided

    [Range(1, int.MaxValue)] // minimum 1
    [JsonPropertyName("attendees")]
    public int? Attendees { get; set; } // "How many people have been trained?"
    
    [Range(1, int.MaxValue)] // minimum 1
    [JsonPropertyName("timeSpent")]
    public int? TimesSpent { get; set; } // "Time spent (in minutes)"

    [Range(1, int.MaxValue)] // minimum 1
    [JsonPropertyName("impact")]
    public int? Impact { get; set; } // "What was your impact in views, reads, attendees, etc.? Add a number below:"
}