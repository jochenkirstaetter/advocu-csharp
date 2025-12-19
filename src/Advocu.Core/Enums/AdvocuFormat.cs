using System.Text.Json.Serialization;

namespace Advocu;

/// <summary>
/// Specifies the format of a feedback activity.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AdvocuFormat>))]
public enum AdvocuFormat
{
    /// <summary>
    /// Survey format.
    /// </summary>
    Survey,
    /// <summary>
    /// Video feedback format.
    /// </summary>
    [JsonStringEnumMemberName("Video feedback")]
    VideoFeedback,
    /// <summary>
    /// In-person feedback sessions format.
    /// </summary>
    [JsonStringEnumMemberName("In-person feedback sessions")]
    InPersonFeedbackSessions,
    /// <summary>
    /// Online feedback sessions format.
    /// </summary>
    [JsonStringEnumMemberName("Online feedback sessions")]
    OnlineFeedbackSessions,
    /// <summary>
    /// Feedback summits format.
    /// </summary>
    [JsonStringEnumMemberName("Feedback summits")]
    FeedbackSummits,
    /// <summary>
    /// User study or focus group format.
    /// </summary>
    [JsonStringEnumMemberName("User study / focus group")]
    UserStudyFocusGroup,
    /// <summary>
    /// Feedback submissions format.
    /// </summary>
    [JsonStringEnumMemberName("Feedback submissions")]
    FeedbackSubmissions,
    /// <summary>
    /// Other format.
    /// </summary>
    Other
}