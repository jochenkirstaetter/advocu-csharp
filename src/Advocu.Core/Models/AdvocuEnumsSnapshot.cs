using System.Reflection;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Advocu;

/// <summary>
/// Represents a consolidated snapshot of all Advocu API enumerations and their exact API string values.
/// </summary>
public sealed class AdvocuEnumsSnapshot
{
    /// <summary>Gets or sets all allowed activity tags for drafting activities.</summary>
    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = [];

    /// <summary>Gets or sets all allowed content types for content creation activities.</summary>
    [JsonPropertyName("contentCreationContentTypes")]
    public List<string> ContentCreationContentTypes { get; set; } = [];

    /// <summary>Gets or sets all allowed content types for product feedback activities.</summary>
    [JsonPropertyName("productFeedbackContentTypes")]
    public List<string> ProductFeedbackContentTypes { get; set; } = [];

    /// <summary>Gets or sets all allowed event formats (mentoring, public speaking, workshop).</summary>
    [JsonPropertyName("eventFormats")]
    public List<string> EventFormats { get; set; } = [];

    /// <summary>Gets or sets all allowed feedback formats (interaction with Googlers).</summary>
    [JsonPropertyName("interactionFormats")]
    public List<string> InteractionFormats { get; set; } = [];

    /// <summary>Gets or sets all allowed interaction types (interaction with Googlers).</summary>
    [JsonPropertyName("interactionTypes")]
    public List<string> InteractionTypes { get; set; } = [];

    /// <summary>Gets or sets all allowed significance types (stories).</summary>
    [JsonPropertyName("significanceTypes")]
    public List<string> SignificanceTypes { get; set; } = [];

    /// <summary>Gets or sets all activity type slugs.</summary>
    [JsonPropertyName("activityTypes")]
    public List<string> ActivityTypes { get; set; } = [];

    /// <summary>Gets or sets all country values.</summary>
    [JsonPropertyName("countries")]
    public List<string> Countries { get; set; } = [];

    /// <summary>
    /// Creates a snapshot containing all Advocu enumeration values as their exact API strings.
    /// </summary>
    public static AdvocuEnumsSnapshot Create() => new()
    {
        Tags = GetApiValues<AdvocuTag>(),
        ContentCreationContentTypes =
        [
            GetApiValue(AdvocuActivityContentType.Articles),
            GetApiValue(AdvocuActivityContentType.Books),
            GetApiValue(AdvocuActivityContentType.CodeContribution),
            GetApiValue(AdvocuActivityContentType.Demos),
            GetApiValue(AdvocuActivityContentType.Newsletters),
            GetApiValue(AdvocuActivityContentType.Podcasts),
            GetApiValue(AdvocuActivityContentType.Videos)
        ],
        ProductFeedbackContentTypes =
        [
            GetApiValue(AdvocuActivityContentType.EarlyAccessProgram),
            GetApiValue(AdvocuActivityContentType.ProductFeedbackSession)
        ],
        EventFormats = GetApiValues<AdvocuEventFormat>(),
        InteractionFormats = GetApiValues<AdvocuFormat>(),
        InteractionTypes = GetApiValues<AdvocuInteractionType>(),
        SignificanceTypes = GetApiValues<AdvocuSignificanceType>(),
        ActivityTypes = GetApiValues<AdvocuSlug>(),
        Countries = GetApiValues<AdvocuCountry>()
    };

    /// <summary>
    /// Gets the exact API string values for all members of the specified enum type.
    /// </summary>
    public static List<string> GetApiValues<T>() where T : struct, Enum =>
        [.. Enum.GetValues<T>().Select(GetApiValue)];

    /// <summary>
    /// Gets the exact API string value for the specified enum member.
    /// </summary>
    public static string GetApiValue<T>(T value) where T : struct, Enum
    {
        var name = value.ToString();
        var member = typeof(T).GetMember(name).FirstOrDefault();
        var attr = member?.GetCustomAttribute<JsonStringEnumMemberNameAttribute>();
        return attr?.Name ?? name;
    }
}
