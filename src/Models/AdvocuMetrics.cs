using System.ComponentModel.DataAnnotations; // For data annotations if needed for validation
using System.Text.Json.Serialization;

namespace Advocu;

public class AdvocuMetrics
{
    [Range(1, int.MaxValue)] // Minimum 1 for metricsReaders
    [JsonPropertyName("readers")]
    public int? Readers { get; set; } // Nullable int as it might be optional based on context, but min 1 if provided

    [Required]
    [Range(1, int.MaxValue)] // minimum 1
    [JsonPropertyName("attendees")]
    public int? Attendees { get; set; } // "How many people have been trained?"
}