using System.Text.Json.Serialization;

namespace Advocu;

[JsonConverter(typeof(JsonStringEnumConverter<AdvocuSignificanceType>))]
public enum AdvocuSignificanceType
{
    [JsonStringEnumMemberName("Diversity & Inclusion")]
    DiversityInclusion,
    [JsonStringEnumMemberName("Helping Business")]
    HelpingBusiness,
    [JsonStringEnumMemberName("Social Impact")]
    SocialImpact,
    [JsonStringEnumMemberName("Feedback to Google")]
    FeedbackToGoogle,
    [JsonStringEnumMemberName("Community Leading")]
    CommunityLeading,
    [JsonStringEnumMemberName("Technology / Open source")]
    TechnologyOpenSource
}