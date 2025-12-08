using System.Text.Json.Serialization;

namespace Advocu;

[JsonConverter(typeof(JsonStringEnumConverter<AdvocuEventFormat>))]
public enum AdvocuEventFormat
{
    [JsonStringEnumMemberName("In-Person")]
    InPerson,
    Virtual,
    Hybrid
}
