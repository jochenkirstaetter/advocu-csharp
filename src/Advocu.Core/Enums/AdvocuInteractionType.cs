using System.Text.Json.Serialization;

namespace Advocu;

[JsonConverter(typeof(JsonStringEnumConverter<AdvocuInteractionType>))]
public enum AdvocuInteractionType
{
    [JsonStringEnumMemberName("Product feedback (research studies, roadmap input, Customer Advisory Boards, EAP)")]
    ProductFeedbackResearchStudiesRoadmapInputCustomerAdvisoryBoardsEap,
    [JsonStringEnumMemberName("File bugs / help out with finding bugs")]
    FileBugsHelpOutWithFindingBugs,
    GitHub,
    [JsonStringEnumMemberName("Stack Overflow")]
    StackOverflow,
    Industry,
    [JsonStringEnumMemberName("Other interactions with Google Product Teams")]
    OtherInteractionsWithGoogleProductTeams
}