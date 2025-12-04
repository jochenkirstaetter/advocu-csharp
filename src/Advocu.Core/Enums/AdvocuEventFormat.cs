using System.Text.Json.Serialization;

namespace Advocu;

public enum AdvocuEventFormat
{
    [JsonStringEnumMemberName("In-Person")]
    InPerson,
    Virtual,
    Hybrid
}
