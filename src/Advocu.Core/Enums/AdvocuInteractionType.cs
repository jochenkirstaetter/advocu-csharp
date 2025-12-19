using System.Text.Json.Serialization;

namespace Advocu;

/// <summary>
/// Specifies the type of interaction with Googlers.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AdvocuInteractionType>))]
public enum AdvocuInteractionType
{
    /// <summary>
    /// Product feedback, research studies, roadmap input, customer advisory boards, EAP.
    /// </summary>
    [JsonStringEnumMemberName("Product feedback (research studies, roadmap input, Customer Advisory Boards, EAP)")]
    ProductFeedbackResearchStudiesRoadmapInputCustomerAdvisoryBoardsEap,
    /// <summary>
    /// File bugs or help with finding bugs.
    /// </summary>
    [JsonStringEnumMemberName("File bugs / help out with finding bugs")]
    FileBugsHelpOutWithFindingBugs,
    /// <summary>
    /// GitHub interaction.
    /// </summary>
    GitHub,
    /// <summary>
    /// Stack Overflow interaction.
    /// </summary>
    [JsonStringEnumMemberName("Stack Overflow")]
    StackOverflow,
    /// <summary>
    /// Industry interaction.
    /// </summary>
    Industry,
    /// <summary>
    /// Other interactions with Google Product Teams.
    /// </summary>
    [JsonStringEnumMemberName("Other interactions with Google Product Teams")]
    OtherInteractionsWithGoogleProductTeams
}