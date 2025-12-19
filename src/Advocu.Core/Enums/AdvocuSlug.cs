using System.Text.Json.Serialization;

namespace Advocu;

/// <summary>
/// Specifies the activity slug.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AdvocuSlug>))]
public enum AdvocuSlug
{
    /// <summary>
    /// Content Creation activity.
    /// </summary>
    [JsonStringEnumMemberName("Content Creation")]
    ContentCreation,
    /// <summary>
    /// Public Speaking activity.
    /// </summary>
    [JsonStringEnumMemberName("Public Speaking")]
    PublicSpeaking,
    /// <summary>
    /// Workshop activity.
    /// </summary>
    Workshop,
    /// <summary>
    /// Mentoring activity.
    /// </summary>
    Mentoring,
    /// <summary>
    /// Product Feedback Given activity.
    /// </summary>
    [JsonStringEnumMemberName("Product Feedback Given")]
    ProductFeedbackGiven,
    /// <summary>
    /// Interaction With Googlers activity.
    /// </summary>
    [JsonStringEnumMemberName("Interaction With Googlers")]
    InteractionWithGooglers,
    /// <summary>
    /// Stories activity.
    /// </summary>
    Stories,
    /// <summary>
    /// GitHub Repository activity.
    /// </summary>
    [JsonStringEnumMemberName("GitHub Repository")]
    GithubRepository,
    /// <summary>
    /// Youtube Video activity.
    /// </summary>
    [JsonStringEnumMemberName("Youtube Video")]
    YoutubeVideo
}