using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using ModelContextProtocol.Server;

namespace Advocu.Mcp.Shared;

/// <summary>
/// Defines MCP resources exposing Advocu enumerations, activity schemas, and live activity context.
/// </summary>
[McpServerResourceType]
public sealed class AdvocuResources(AdvocuApiClient apiClient)
{
    private readonly AdvocuApiClient _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Returns all Advocu API enumerations and their exact allowed values in a single JSON document.
    /// </summary>
    [McpServerResource(UriTemplate = "advocumcp://advocu/enums", Name = "AdvocuEnums", MimeType = "application/json")]
    [Description("All Advocu API enumerations (tags, content types, event formats, interaction formats, interaction types, significance types, activity types, and countries) in one JSON payload.")]
    public static string GetAllEnums()
    {
        return JsonSerializer.Serialize(AdvocuEnumsSnapshot.Create(), SerializerOptions);
    }

    /// <summary>
    /// Returns machine-readable field rules, constraints, and conditional validation rules for all activity types.
    /// </summary>
    [McpServerResource(UriTemplate = "advocumcp://advocu/schemas", Name = "AdvocuActivitySchemas", MimeType = "application/json")]
    [Description("Field requirements, length limits, regex patterns, metrics, and conditional rules for all Advocu activity types.")]
    public static string GetActivitySchemas()
    {
        return JsonSerializer.Serialize(AdvocuActivitySchemas.All(), SerializerOptions);
    }

    /// <summary>
    /// Returns a compact list of recent activities from Advocu for duplicate checking and style reference.
    /// </summary>
    [McpServerResource(UriTemplate = "advocumcp://advocu/activities/recent", Name = "AdvocuRecentActivities", MimeType = "application/json")]
    [Description("Recent submitted Advocu activities (compact summaries with activityId, type, date, title, and tags) for duplicate checking and context.")]
    public async Task<string> GetRecentActivitiesAsync(CancellationToken cancellationToken = default)
    {
        var response = await _apiClient.GetActivitiesAsync(new GetActivitiesQuery { Page = 0, Size = 20 }, cancellationToken);
        var summaries = response.Content.Select(ActivitySummary.FromItem).ToList();
        return JsonSerializer.Serialize(summaries, SerializerOptions);
    }

    /// <summary>
    /// Looks up a single activity by its identifier.
    /// </summary>
    [McpServerResource(UriTemplate = "advocumcp://advocu/activities/{activityId}", Name = "AdvocuActivityById", MimeType = "application/json")]
    [Description("Full details of a single Advocu activity identified by its activityId.")]
    public async Task<string> GetActivityByIdAsync(
        [Description("The unique activityId to inspect.")] string activityId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(activityId);

        const int maxPages = 10;
        for (var page = 0; page < maxPages; page++)
        {
            var batch = await _apiClient.GetActivitiesAsync(new GetActivitiesQuery { Page = page, Size = 100 }, cancellationToken);
            var match = batch.Content.FirstOrDefault(a => string.Equals(a.ActivityId, activityId, StringComparison.OrdinalIgnoreCase));
            if (match is not null)
            {
                return JsonSerializer.Serialize(match, SerializerOptions);
            }

            if (batch.Content.Count < 100)
            {
                break;
            }
        }

        throw new KeyNotFoundException($"Activity '{activityId}' was not found.");
    }
}
