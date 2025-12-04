using System.Text.Json.Serialization;

namespace Advocu;

public enum AdvocuInteractionType
{
    [JsonStringEnumMemberName("Product feedback (research studies, roadmap input, Customer Advisory Boards, EAP)")]
    Product_feedback_research_studies_roadmap_input_Customer_Advisory_Boards_EAP,
    [JsonStringEnumMemberName("File bugs / help out with finding bugs")]
    File_bugs_help_out_with_finding_bugs,
    GitHub,
    [JsonStringEnumMemberName("Stack Overflow")]
    Stack_Overflow,
    Industry,
    [JsonStringEnumMemberName("Other interactions with Google Product Teams")]
    Other_Interactions_with_Google_Product_Teams
}