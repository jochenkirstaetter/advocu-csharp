using System.ComponentModel.DataAnnotations; // For data annotations if needed for validation
using System.Text.Json.Serialization; // For JSON serialization attributes

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Advocu;

/// <summary>
/// Represents a request to create a Content Creation activity.
/// </summary>
public sealed class CreateContentCreationActivityRequest : ActivityRequest, IValidatableObject
{
    /// <summary>
    /// Gets or sets the number of readers or views for the content.
    /// </summary>
    [JsonIgnore]
    public int? Readers
    {
        get => Metrics.Readers;
        set => Metrics.Readers = Math.Max(value ?? 1, 1);
    }

    /// <summary>
    /// Gets or sets the type of content created (e.g., Article, Video).
    /// </summary>
    [Required]
    [JsonPropertyName("contentType")]
    [JsonConverter(typeof(JsonStringEnumConverter<AdvocuActivityContentType>))] // Serializes enum as string
    public AdvocuActivityContentType ContentType { get; set; }

    /// <summary>
    /// Gets or sets the date of the activity in YYYY-MM-DD format.
    /// </summary>
    [Required]
    [RegularExpression(@"^[0-9]{4}-[0-9]{2}-[0-9]{2}$", ErrorMessage = "ActivityDate must match YYYY-MM-DD format.")]
    [JsonPropertyName("activityDate")]
    public required string ActivityDate { get; set; } // Pattern yyyy-MM-dd, will format DateTime to this string

    /// <summary>
    /// Gets or sets the URL of the activity.
    /// </summary>
    [Required]
    [MaxLength(500)]
    [RegularExpression(@"^https?:\/\/(www\.)?.*$", ErrorMessage = "ActivityUrl must be a valid HTTP/HTTPS URL.")]
    [JsonPropertyName("activityUrl")]
    [Url]
    public required string ActivityUrl { get; set; }

    /// <inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ContentType is AdvocuActivityContentType.EarlyAccessProgram or AdvocuActivityContentType.ProductFeedbackSession)
        {
            yield return new ValidationResult(
                $"ContentType '{ContentType}' is only valid for Product Feedback activities, not Content Creation.",
                [nameof(ContentType)]);
        }
    }
}