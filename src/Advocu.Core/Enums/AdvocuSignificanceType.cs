using System.Text.Json.Serialization;

namespace Advocu;

/// <summary>
/// Specifies the type of significance for an activity.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AdvocuSignificanceType>))]
public enum AdvocuSignificanceType
{
    /// <summary>
    /// Diversity and Inclusion.
    /// </summary>
    [JsonStringEnumMemberName("Diversity & Inclusion")]
    DiversityInclusion,
    /// <summary>
    /// Helping Business.
    /// </summary>
    [JsonStringEnumMemberName("Helping Business")]
    HelpingBusiness,
    /// <summary>
    /// Social Impact.
    /// </summary>
    [JsonStringEnumMemberName("Social Impact")]
    SocialImpact,
    /// <summary>
    /// Feedback to Google.
    /// </summary>
    [JsonStringEnumMemberName("Feedback to Google")]
    FeedbackToGoogle,
    /// <summary>
    /// Community Leading.
    /// </summary>
    [JsonStringEnumMemberName("Community Leading")]
    CommunityLeading,
    /// <summary>
    /// Technology and Open Source.
    /// </summary>
    [JsonStringEnumMemberName("Technology / Open source")]
    TechnologyOpenSource
}