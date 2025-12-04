using System.Text.Json.Serialization;

namespace Advocu;

public enum AdvocuFormat
{
    Survey,
    [JsonStringEnumMemberName("Video feedback")]
    VideoFeedback,
    [JsonStringEnumMemberName("In-person feedback sessions")]
    InPersonFeedbackSessions,
    [JsonStringEnumMemberName("Online feedback sessions")]
    OnlineFeedbackSessions,
    [JsonStringEnumMemberName("Feedback summits")]
    FeedbackSummits,
    [JsonStringEnumMemberName("User study / focus group")]
    UserStudyFocusGroup,
    [JsonStringEnumMemberName("Feedback submissions")]
    FeedbackSubmissions,
    Other
}