using System.Text.Json.Serialization;

namespace Advocu;

public enum AdvocuSignificanceType
{
    [JsonStringEnumMemberName("Diversity & Inclusion")]
    Diversity_Inclusion,
    [JsonStringEnumMemberName("Helping Business")]
    Helping_Business,
    [JsonStringEnumMemberName("Social Impact")]
    Social_Impact,
    [JsonStringEnumMemberName("Feedback to Google")]
    Feedback_to_Google,
    [JsonStringEnumMemberName("Community Leading")]
    Community_Leading,
    [JsonStringEnumMemberName("Technology / Open source")]
    Technology_Open_source
}