using System.Reflection;
using System.Text.Json.Serialization;

namespace Advocu.WebApp.Extensions;

/// <summary>
/// Provides extension methods for enumerations.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the display name of the enum value.
    /// </summary>
    /// <param name="enumValue">The enum value.</param>
    /// <returns>The display name.</returns>
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<JsonStringEnumMemberNameAttribute>()?
            .Name ?? enumValue.ToString();
    }
}
