using System.Text.Json.Serialization;

namespace Advocu;

[JsonConverter(typeof(JsonStringEnumConverter<AdvocuActivityContentType>))]
public enum AdvocuActivityContentType
{
    Articles,
    Books,
    [JsonStringEnumMemberName("Code contribution")]
    CodeContribution,
    [JsonStringEnumMemberName("Console Projects")]
    ConsoleProjects,
    Demos,
    Newsletters,
    Podcasts,
    Videos,
    [JsonStringEnumMemberName("Reusable GCP presentations")]
    ReusableGcpPresentations,
    [JsonStringEnumMemberName("Early access program")]
    EarlyAccessProgram,
    [JsonStringEnumMemberName("Product feedback session")]
    ProductFeedbackSession
}