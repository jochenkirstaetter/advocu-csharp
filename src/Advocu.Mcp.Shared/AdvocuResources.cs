using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using ModelContextProtocol;
using ModelContextProtocol.Server;

namespace Advocu.Mcp.Shared;

/// <summary>
/// Response envelope for <c>advocumcp://advocu/activities/recent</c>.
/// </summary>
public sealed class RecentActivitiesResult
{
    [JsonPropertyName("count")]
    public required int Count { get; init; }

    [JsonPropertyName("truncated")]
    public required bool Truncated { get; init; }

    [JsonPropertyName("warning")]
    public string? Warning { get; init; }

    [JsonPropertyName("activities")]
    public required List<ActivitySummary> Activities { get; init; }
}

/// <summary>
/// Defines MCP resources exposing Advocu enumerations, activity schemas, and live activity context.
/// </summary>
[McpServerResourceType]
public sealed class AdvocuResources(AdvocuApiClient apiClient)
{
    private readonly AdvocuApiClient _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));

    // Intentional pagination safety cap to respect the Advocu Personal API rate limit
    // (30 requests per minute per IP; HTTP 429 on excess). Leaving this unbounded would allow
    // a single resource lookup to exhaust the account's per-minute API quota.
    private const int MaxApiScanPages = 25;
    private const int MaxPageSize = 100;
    private const int RecentCount = 20;

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
    /// Returns a compact list of the newest 20 activities from Advocu for duplicate checking and style reference.
    /// </summary>
    [McpServerResource(UriTemplate = "advocumcp://advocu/activities/recent", Name = "AdvocuRecentActivities", MimeType = "application/json")]
    [Description("Newest 20 submitted Advocu activities (compact summaries ordered newest first, with truncation metadata if the API rate-limit scan cap was reached).")]
    public async Task<string> GetRecentActivitiesAsync(CancellationToken cancellationToken = default)
    {
        // GET /activities returns results sorted ascending by submissionDate.
        // Query the last 90 days first to stay well under the 30 req/min rate limit.
        var recentWindowStart = DateTimeOffset.UtcNow.AddDays(-90);
        List<ActivityItem> collected = [];
        var truncated = false;

        for (var page = 0; page < MaxApiScanPages; page++)
        {
            var batch = await _apiClient.GetActivitiesAsync(new GetActivitiesQuery
            {
                From = recentWindowStart,
                Page = page,
                Size = MaxPageSize
            }, cancellationToken);

            collected.AddRange(batch.Content);
            if (batch.Content.Count < MaxPageSize)
            {
                break;
            }

            if (page == MaxApiScanPages - 1)
            {
                truncated = true;
            }
        }

        // If fewer than RecentCount activities exist in the last 90 days, top up from earlier history
        // while staying within the rate-limit-safe page cap.
        if (collected.Count < RecentCount && !truncated)
        {
            List<ActivityItem> earlier = [];
            for (var page = 0; page < MaxApiScanPages; page++)
            {
                var batch = await _apiClient.GetActivitiesAsync(new GetActivitiesQuery
                {
                    To = recentWindowStart,
                    Page = page,
                    Size = MaxPageSize
                }, cancellationToken);

                earlier.AddRange(batch.Content);
                if (batch.Content.Count < MaxPageSize)
                {
                    break;
                }

                if (page == MaxApiScanPages - 1)
                {
                    truncated = true;
                }
            }

            var needed = RecentCount - collected.Count;
            collected = [.. earlier.TakeLast(needed), .. collected];
        }

        var newest = collected
            .TakeLast(RecentCount)
            .Reverse()
            .Select(ActivitySummary.FromItem)
            .ToList();

        var result = new RecentActivitiesResult
        {
            Count = newest.Count,
            Truncated = truncated,
            Warning = truncated
                ? $"Scan reached the {MaxApiScanPages * MaxPageSize}-activity safety cap (Advocu API rate limit: 30 req/min); newer activities may exist. Use `search_activities` with a narrower `from`/`to` window."
                : null,
            Activities = newest
        };

        return JsonSerializer.Serialize(result, SerializerOptions);
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

        // Intentionally capped at MaxApiScanPages (2,500 activities) to respect the Advocu Personal API
        // rate limit of 30 requests/minute per IP and prevent exhausting quota on a missing/typo'd ID.
        for (var page = 0; page < MaxApiScanPages; page++)
        {
            var batch = await _apiClient.GetActivitiesAsync(new GetActivitiesQuery
            {
                Page = page,
                Size = MaxPageSize
            }, cancellationToken);

            var match = batch.Content.FirstOrDefault(a => string.Equals(a.ActivityId, activityId, StringComparison.OrdinalIgnoreCase));
            if (match is not null)
            {
                return JsonSerializer.Serialize(match, SerializerOptions);
            }

            if (batch.Content.Count < MaxPageSize)
            {
                throw new McpException($"Activity '{activityId}' was not found.");
            }
        }

        throw new McpException(
            $"Activity '{activityId}' was not found within the first {MaxApiScanPages * MaxPageSize} activities. " +
            "Pagination is intentionally capped to respect the Advocu Personal API rate limit (30 requests/minute). " +
            "Use `search_activities` with `from`/`to` date filters to locate activities outside this scan window.");
    }
}
