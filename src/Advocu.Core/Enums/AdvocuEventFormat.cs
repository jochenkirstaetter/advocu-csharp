using System.Text.Json.Serialization;

namespace Advocu;

/// <summary>
/// Specifies the format of an event.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AdvocuEventFormat>))]
public enum AdvocuEventFormat
{
    /// <summary>
    /// In-person event.
    /// </summary>
    [JsonStringEnumMemberName("In-Person")]
    InPerson,
    /// <summary>
    /// Virtual event.
    /// </summary>
    Virtual,
    /// <summary>
    /// Hybrid event.
    /// </summary>
    Hybrid
}
