using System.Text.Json.Serialization;

namespace Advocu;

/// <summary>
/// Specifies the type of content created for an activity.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AdvocuActivityContentType>))]
public enum AdvocuActivityContentType
{
    /// <summary>
    /// Articles content.
    /// </summary>
    Articles,
    /// <summary>
    /// Books content.
    /// </summary>
    Books,
    /// <summary>
    /// Code contribution content.
    /// </summary>
    [JsonStringEnumMemberName("Code contribution")]
    CodeContribution,
    /// <summary>
    /// Console projects content.
    /// </summary>
    [JsonStringEnumMemberName("Console Projects")]
    ConsoleProjects,
    /// <summary>
    /// Demos content.
    /// </summary>
    Demos,
    /// <summary>
    /// Newsletters content.
    /// </summary>
    Newsletters,
    /// <summary>
    /// Podcasts content.
    /// </summary>
    Podcasts,
    /// <summary>
    /// Videos content.
    /// </summary>
    Videos,
    /// <summary>
    /// Reusable GCP presentations content.
    /// </summary>
    [JsonStringEnumMemberName("Reusable GCP presentations")]
    ReusableGcpPresentations,
    /// <summary>
    /// Early access program content.
    /// </summary>
    [JsonStringEnumMemberName("Early access program")]
    EarlyAccessProgram,
    /// <summary>
    /// Product feedback session content.
    /// </summary>
    [JsonStringEnumMemberName("Product feedback session")]
    ProductFeedbackSession
}