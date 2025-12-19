using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;
using Spectre.Console;

namespace Advocu.NuGet.Parsing;

internal static class AdvocuEnumParser
{
    public static T Parse<T>(string input) where T : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentNullException(nameof(input));
        }

        // 1. Try direct Enum parsing (case-insensitive)
        if (Enum.TryParse<T>(input, true, out var result))
        {
            return result;
        }

        // 2. Try matching JsonStringEnumMemberNameAttribute
        var fields = typeof(T).GetFields();
        foreach (var field in fields)
        {
            var attr = field.GetCustomAttribute<JsonStringEnumMemberNameAttribute>();
            if (attr != null && string.Equals(attr.Name, input, StringComparison.OrdinalIgnoreCase))
            {
                return (T)field.GetValue(null)!;
            }
        }

        // 3. No match found - Suggest close matches
        var candidates = new List<string>(Enum.GetNames<T>());
        foreach (var field in fields)
        {
            var attr = field.GetCustomAttribute<JsonStringEnumMemberNameAttribute>();
            if (attr != null)
            {
                candidates.Add(attr.Name);
            }
        }

        var suggestion = GetClosestMatch(input, candidates);
        var errorMsg = $"Invalid value: '{input}'.";
        
        if (!string.IsNullOrEmpty(suggestion))
        {
            errorMsg += $" Did you mean '{suggestion}'?";
        }

        // For large Enums like Tags, don't list everything.
        // For others, we could, but let's stick to the suggestion pattern for consistency and brevity.
        errorMsg += $" Use 'advocu list' commands to see available options.";

        throw new ArgumentException(errorMsg);
    }

    private static string? GetClosestMatch(string input, IEnumerable<string> possibilities)
    {
        var inputLower = input.ToLowerInvariant();
        int minDistance = int.MaxValue;
        string? closestRequest = null;

        foreach (var p in possibilities)
        {
            var distance = ComputeLevenshteinDistance(inputLower, p.ToLowerInvariant());
            if (distance < minDistance)
            {
                minDistance = distance;
                closestRequest = p;
            }
        }

        // Only suggest if reasonable similarity (e.g. within 3 edits)
        return minDistance <= 3 ? closestRequest : null;
    }

    private static int ComputeLevenshteinDistance(string s, string t)
    {
        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        if (n == 0) return m;
        if (m == 0) return n;

        for (int i = 0; i <= n; d[i, 0] = i++) { }
        for (int j = 0; j <= m; d[0, j] = j++) { }

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }
        return d[n, m];
    }
}

internal class AdvocuEnumConverter<T> : TypeConverter where T : struct, Enum
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string s)
        {
            try 
            {
                return AdvocuEnumParser.Parse<T>(s);
            }
            catch (ArgumentException ex)
            {
                // Throwing FormatException often plays better with CLI parsers expecting standard conversion failures
                // But Spectre.Console usually bubbles up exceptions efficiently.
                // Re-throwing so the message is preserved.
                throw new FormatException(ex.Message, ex);
            }
        }
        return base.ConvertFrom(context, culture, value);
    }
}
