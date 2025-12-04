using System.Reflection;
using System.Text.Json.Serialization;

namespace Advocu.WebApp.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<JsonStringEnumMemberNameAttribute>()?
            .Name ?? enumValue.ToString();
    }
}
