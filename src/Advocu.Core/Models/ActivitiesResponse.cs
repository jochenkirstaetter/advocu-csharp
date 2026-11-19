using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Advocu;

/// <summary>
/// Represents the paginated response returned by <c>GET /activities</c>.
/// </summary>
public sealed class ActivitiesResponse
{
    /// <summary>
    /// Gets or sets the list of activities in the current page.
    /// </summary>
    [JsonPropertyName("content")]
    public List<ActivityItem> Content { get; set; } = [];
}

/// <summary>
/// Represents an activity returned by <c>GET /activities</c> or <c>PATCH /activities/{activityId}</c>.
/// </summary>
public sealed class ActivityItem
{
    /// <summary>
    /// Gets or sets the unique identifier of the activity.
    /// </summary>
    [JsonPropertyName("activityId")]
    public string ActivityId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the activity type slug (for example, <c>content-creation</c> or <c>public-speaking</c>).
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the activity payload data.
    /// </summary>
    [JsonPropertyName("data")]
    public ActivityData Data { get; set; } = new();

    /// <summary>
    /// Gets or sets the UTC submission timestamp of the activity.
    /// </summary>
    [JsonPropertyName("submissionDate")]
    public string SubmissionDate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the UTC last-updated timestamp of the activity.
    /// </summary>
    [JsonPropertyName("updatedDate")]
    public string UpdatedDate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the optional UTC archive timestamp of the activity.
    /// </summary>
    [JsonPropertyName("archiveDate")]
    public string? ArchiveDate { get; set; }
}

/// <summary>
/// Represents the data payload of an activity across all supported activity types.
/// </summary>
public sealed class ActivityData
{
    /// <summary>
    /// Gets or sets the title of the activity.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the rich-text description of the activity.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the plain-text description of the activity.
    /// </summary>
    [JsonPropertyName("description_plain_text")]
    public string? DescriptionPlainText { get; set; }

    /// <summary>
    /// Gets or sets the content type (for content creation or product feedback activities).
    /// </summary>
    [JsonPropertyName("contentType")]
    public string? ContentType { get; set; }

    /// <summary>
    /// Gets or sets the product description (for product feedback activities).
    /// </summary>
    [JsonPropertyName("productDescription")]
    public string? ProductDescription { get; set; }

    /// <summary>
    /// Gets or sets the plain-text product description (for product feedback activities).
    /// </summary>
    [JsonPropertyName("productDescription_plain_text")]
    public string? ProductDescriptionPlainText { get; set; }

    /// <summary>
    /// Gets or sets the feedback format (for interaction with Googlers activities).
    /// </summary>
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    /// <summary>
    /// Gets or sets the interaction type (for interaction with Googlers activities).
    /// </summary>
    [JsonPropertyName("interactionType")]
    public string? InteractionType { get; set; }

    /// <summary>
    /// Gets or sets the event format (for mentoring, public speaking, and workshop activities).
    /// </summary>
    [JsonPropertyName("eventFormat")]
    public string? EventFormat { get; set; }

    /// <summary>
    /// Gets or sets the country name.
    /// </summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the two-letter ISO country code.
    /// </summary>
    [JsonPropertyName("countryIso")]
    public string? CountryIso { get; set; }

    /// <summary>
    /// Gets or sets the city name.
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the city place identifier.
    /// </summary>
    [JsonPropertyName("cityPlaceId")]
    public string? CityPlaceId { get; set; }

    /// <summary>
    /// Gets or sets the state or region of the city.
    /// </summary>
    [JsonPropertyName("cityState")]
    public string? CityState { get; set; }

    /// <summary>
    /// Gets or sets the number of in-person attendees for hybrid events.
    /// </summary>
    [JsonPropertyName("inPersonAttendees")]
    public int? InPersonAttendees { get; set; }

    /// <summary>
    /// Gets or sets the explanation of why a story is significant.
    /// </summary>
    [JsonPropertyName("whyIsSignificant")]
    public string? WhyIsSignificant { get; set; }

    /// <summary>
    /// Gets or sets the plain-text explanation of why a story is significant.
    /// </summary>
    [JsonPropertyName("whyIsSignificant_plain_text")]
    public string? WhyIsSignificantPlainText { get; set; }

    /// <summary>
    /// Gets or sets the significance type for stories.
    /// </summary>
    [JsonPropertyName("significanceType")]
    public string? SignificanceType { get; set; }

    /// <summary>
    /// Gets or sets the tags associated with the activity (stored as strings to preserve historical/retired tags).
    /// </summary>
    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Gets or sets the labels/topics (for GitHub repository activities).
    /// </summary>
    [JsonPropertyName("labels")]
    public List<string>? Labels { get; set; }

    /// <summary>
    /// Gets or sets the activity metrics.
    /// </summary>
    [JsonPropertyName("metrics")]
    public ActivityItemMetrics? Metrics { get; set; }

    /// <summary>
    /// Gets or sets the activity date (<c>yyyy-MM-dd</c>).
    /// </summary>
    [JsonPropertyName("activityDate")]
    public string? ActivityDate { get; set; }

    /// <summary>
    /// Gets or sets the activity URL.
    /// </summary>
    [JsonPropertyName("activityUrl")]
    public string? ActivityUrl { get; set; }

    /// <summary>
    /// Gets or sets the optional image URL.
    /// </summary>
    [JsonPropertyName("imageUrl")]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Gets or sets additional rich-text information.
    /// </summary>
    [JsonPropertyName("additionalInfo")]
    public string? AdditionalInfo { get; set; }

    /// <summary>
    /// Gets or sets additional plain-text information.
    /// </summary>
    [JsonPropertyName("additionalInfo_plain_text")]
    public string? AdditionalInfoPlainText { get; set; }

    /// <summary>
    /// Gets or sets additional rich-text links (for interaction with Googlers activities).
    /// </summary>
    [JsonPropertyName("additionalLinks")]
    public string? AdditionalLinks { get; set; }

    /// <summary>
    /// Gets or sets additional plain-text links (for interaction with Googlers activities).
    /// </summary>
    [JsonPropertyName("additionalLinks_plain_text")]
    public string? AdditionalLinksPlainText { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the activity is private.
    /// </summary>
    [JsonPropertyName("private")]
    public bool? Private { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether an external GitHub repository or YouTube video is available.
    /// </summary>
    [JsonPropertyName("available")]
    public bool? Available { get; set; }

    /// <summary>
    /// Gets or sets GitHub repository metadata (for <c>github-repository</c> activities).
    /// </summary>
    [JsonPropertyName("repositoryData")]
    public ActivityRepositoryData? RepositoryData { get; set; }

    /// <summary>
    /// Gets or sets YouTube video metadata (for <c>youtube-video</c> activities).
    /// </summary>
    [JsonPropertyName("videoData")]
    public ActivityVideoData? VideoData { get; set; }

    /// <summary>
    /// Gets or sets any additional unmapped properties returned by the API.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}

/// <summary>
/// Represents metrics returned on an activity item across all activity types.
/// </summary>
public sealed class ActivityItemMetrics
{
    /// <summary>Gets or sets the reader count (content creation).</summary>
    [JsonPropertyName("readers")]
    public int? Readers { get; set; }

    /// <summary>Gets or sets the attendee count (mentoring, public speaking, workshop).</summary>
    [JsonPropertyName("attendees")]
    public int? Attendees { get; set; }

    /// <summary>Gets or sets the time spent in minutes (interaction with Googlers, product feedback).</summary>
    [JsonPropertyName("timeSpent")]
    public int? TimeSpent { get; set; }

    /// <summary>Gets or sets the impact value (stories).</summary>
    [JsonPropertyName("impact")]
    public int? Impact { get; set; }

    /// <summary>Gets or sets the star count (GitHub repository).</summary>
    [JsonPropertyName("stars")]
    public int? Stars { get; set; }

    /// <summary>Gets or sets the fork count (GitHub repository).</summary>
    [JsonPropertyName("forks")]
    public int? Forks { get; set; }

    /// <summary>Gets or sets the watcher count (GitHub repository).</summary>
    [JsonPropertyName("watching")]
    public int? Watching { get; set; }

    /// <summary>Gets or sets the view count (YouTube video).</summary>
    [JsonPropertyName("views")]
    public int? Views { get; set; }

    /// <summary>Gets or sets the like count (YouTube video).</summary>
    [JsonPropertyName("likes")]
    public int? Likes { get; set; }

    /// <summary>Gets or sets the comment count (YouTube video).</summary>
    [JsonPropertyName("comments")]
    public int? Comments { get; set; }
}

/// <summary>
/// Represents GitHub repository metadata on a <c>github-repository</c> activity.
/// </summary>
public sealed class ActivityRepositoryData
{
    /// <summary>Gets or sets the repository ID.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>Gets or sets the repository name.</summary>
    [JsonPropertyName("repository")]
    public string? Repository { get; set; }

    /// <summary>Gets or sets the repository owner.</summary>
    [JsonPropertyName("owner")]
    public string? Owner { get; set; }
}

/// <summary>
/// Represents YouTube video metadata on a <c>youtube-video</c> activity.
/// </summary>
public sealed class ActivityVideoData
{
    /// <summary>Gets or sets the YouTube video ID.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
