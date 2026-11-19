using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Advocu;

/// <summary>
/// Represents optional query parameters for <c>GET /activities</c>.
/// </summary>
public sealed class GetActivitiesQuery
{
    /// <summary>
    /// Gets or sets the inclusive lower bound for submission date filtering (UTC).
    /// </summary>
    public DateTimeOffset? From { get; set; }

    /// <summary>
    /// Gets or sets the exclusive upper bound for submission date filtering (UTC).
    /// </summary>
    public DateTimeOffset? To { get; set; }

    /// <summary>
    /// Gets or sets the lower bound for filtering activities updated after this timestamp (UTC).
    /// </summary>
    public DateTimeOffset? UpdatedAfter { get; set; }

    /// <summary>
    /// Gets or sets the zero-based page number.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int? Page { get; set; }

    /// <summary>
    /// Gets or sets the page size (1 to 100).
    /// </summary>
    [Range(1, 100)]
    public int? Size { get; set; }
}

/// <summary>
/// Represents the request body for <c>PATCH /activities/{activityId}</c>.
/// </summary>
public sealed class PatchActivityRequest
{
    /// <summary>
    /// Gets or sets the partial activity data to update.
    /// </summary>
    [Required]
    [JsonPropertyName("data")]
    public PatchActivityData Data { get; set; } = new();
}

/// <summary>
/// Represents optional fields that can be updated via <c>PATCH /activities/{activityId}</c>.
/// Only non-null properties are serialized.
/// </summary>
public sealed class PatchActivityData : IValidatableObject
{
    /// <summary>
    /// Gets or sets the activity title (3 to 200 characters).
    /// </summary>
    [StringLength(200, MinimumLength = 3)]
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the activity description (up to 2000 characters).
    /// </summary>
    [MaxLength(2000)]
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the content type (for content creation or product feedback activities).
    /// </summary>
    [JsonPropertyName("contentType")]
    public AdvocuActivityContentType? ContentType { get; set; }

    /// <summary>
    /// Gets or sets the product description (up to 500 characters, for product feedback activities).
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("productDescription")]
    public string? ProductDescription { get; set; }

    /// <summary>
    /// Gets or sets the feedback format (for interaction with Googlers activities).
    /// </summary>
    [JsonPropertyName("format")]
    public AdvocuFormat? Format { get; set; }

    /// <summary>
    /// Gets or sets the interaction type (for interaction with Googlers activities).
    /// </summary>
    [JsonPropertyName("interactionType")]
    public AdvocuInteractionType? InteractionType { get; set; }

    /// <summary>
    /// Gets or sets the event format (for mentoring, public speaking, and workshop activities).
    /// </summary>
    [JsonPropertyName("eventFormat")]
    public AdvocuEventFormat? EventFormat { get; set; }

    /// <summary>
    /// Gets or sets the country (accepted when <see cref="EventFormat"/> is In-Person or Hybrid).
    /// </summary>
    [JsonPropertyName("country")]
    public AdvocuCountry? Country { get; set; }

    /// <summary>
    /// Gets or sets the in-person attendee count (accepted when <see cref="EventFormat"/> is Hybrid).
    /// </summary>
    [Range(0, int.MaxValue)]
    [JsonPropertyName("inPersonAttendees")]
    public int? InPersonAttendees { get; set; }

    /// <summary>
    /// Gets or sets why a story is significant (up to 2000 characters, for stories).
    /// </summary>
    [MaxLength(2000)]
    [JsonPropertyName("whyIsSignificant")]
    public string? WhyIsSignificant { get; set; }

    /// <summary>
    /// Gets or sets the significance type (for stories).
    /// </summary>
    [JsonPropertyName("significanceType")]
    public AdvocuSignificanceType? SignificanceType { get; set; }

    /// <summary>
    /// Gets or sets the tags associated with the activity.
    /// </summary>
    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Gets or sets the activity metrics to update.
    /// </summary>
    [JsonPropertyName("metrics")]
    public ActivityMetrics? Metrics { get; set; }

    /// <summary>
    /// Gets or sets the activity date (<c>yyyy-MM-dd</c>).
    /// </summary>
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "ActivityDate must be in YYYY-MM-DD format.")]
    [JsonPropertyName("activityDate")]
    public string? ActivityDate { get; set; }

    /// <summary>
    /// Gets or sets the activity URL (up to 500 characters).
    /// </summary>
    [MaxLength(500)]
    [RegularExpression(@"^https?:\/\/(www\.)?.*$", ErrorMessage = "ActivityUrl must be a valid HTTP/HTTPS URL.")]
    [JsonPropertyName("activityUrl")]
    public string? ActivityUrl { get; set; }

    /// <summary>
    /// Gets or sets additional information (up to 2000 characters).
    /// </summary>
    [MaxLength(2000)]
    [JsonPropertyName("additionalInfo")]
    public string? AdditionalInfo { get; set; }

    /// <summary>
    /// Gets or sets additional links (up to 2000 characters, for interaction with Googlers activities).
    /// </summary>
    [MaxLength(2000)]
    [JsonPropertyName("additionalLinks")]
    public string? AdditionalLinks { get; set; }

    /// <summary>
    /// Gets or sets whether the activity is private.
    /// </summary>
    [JsonPropertyName("private")]
    public bool? Private { get; set; }

    /// <summary>
    /// Validates cross-field constraints when <see cref="EventFormat"/> is explicitly provided.
    /// </summary>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EventFormat == AdvocuEventFormat.Virtual)
        {
            if (Country.HasValue)
            {
                yield return new ValidationResult(
                    $"Country cannot be provided when EventFormat is {AdvocuEventFormat.Virtual}.",
                    [nameof(Country)]);
            }

            if (InPersonAttendees.HasValue)
            {
                yield return new ValidationResult(
                    $"InPersonAttendees cannot be provided when EventFormat is {AdvocuEventFormat.Virtual}.",
                    [nameof(InPersonAttendees)]);
            }
        }
        else if (EventFormat == AdvocuEventFormat.InPerson && InPersonAttendees.HasValue)
        {
            yield return new ValidationResult(
                $"InPersonAttendees is only accepted for Hybrid events. It cannot be provided when EventFormat is {AdvocuEventFormat.InPerson}.",
                [nameof(InPersonAttendees)]);
        }
    }
}
