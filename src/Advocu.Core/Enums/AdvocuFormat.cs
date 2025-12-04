using System.Text.Json.Serialization;

namespace Advocu;

public enum AdvocuFormat
{
    Survey,
    [JsonStringEnumMemberName("Video feedback")]
    Video_feedback,
    [JsonStringEnumMemberName("In-person feedback sessions")]
    In_person_feedback_sessions,
    [JsonStringEnumMemberName("Online feedback sessions")]
    Online_feedback_sessions,
    [JsonStringEnumMemberName("Feedback summits")]
    Feedback_summits,
    [JsonStringEnumMemberName("User study / focus group")]
    User_study_focus_group,
    [JsonStringEnumMemberName("Feedback submissions")]
    Feedback_submissions,
    Other
}