using System.Text.Json.Serialization;

namespace Advocu.Mcp.Shared;

/// <summary>
/// Describes the schema, required fields, metrics, and conditional rules for each Advocu activity type.
/// </summary>
public sealed class AdvocuActivitySchemaInfo
{
    [JsonPropertyName("activityType")]
    public required string ActivityType { get; init; }

    [JsonPropertyName("canDraft")]
    public required bool CanDraft { get; init; }

    [JsonPropertyName("canUpdate")]
    public required bool CanUpdate { get; init; }

    [JsonPropertyName("requiredFields")]
    public required List<string> RequiredFields { get; init; }

    [JsonPropertyName("optionalFields")]
    public required List<string> OptionalFields { get; init; }

    [JsonPropertyName("metricField")]
    public string? MetricField { get; init; }

    [JsonPropertyName("conditionalRules")]
    public List<string> ConditionalRules { get; init; } = [];

    [JsonPropertyName("constraints")]
    public Dictionary<string, string> Constraints { get; init; } = [];
}

/// <summary>
/// Provides machine-readable schema documentation for all Advocu activity types.
/// </summary>
public static class AdvocuActivitySchemas
{
    /// <summary>
    /// Returns the complete list of activity schemas.
    /// </summary>
    public static List<AdvocuActivitySchemaInfo> All() =>
    [
        new()
        {
            ActivityType = "content-creation",
            CanDraft = true,
            CanUpdate = true,
            RequiredFields = ["title", "description", "contentType", "activityDate", "activityUrl", "readers"],
            OptionalFields = ["tags", "additionalInfo", "private"],
            MetricField = "readers (>= 1)",
            ConditionalRules =
            [
                "contentType must be one of contentCreationContentTypes (Articles, Books, Code contribution, Demos, Newsletters, Podcasts, Videos)."
            ],
            Constraints = CommonConstraints()
        },
        new()
        {
            ActivityType = "public-speaking",
            CanDraft = true,
            CanUpdate = true,
            RequiredFields = ["title", "description", "eventFormat", "activityDate", "activityUrl", "attendees"],
            OptionalFields = ["country", "city", "cityPlaceId", "inPersonAttendees", "tags", "additionalInfo", "private"],
            MetricField = "attendees (>= 1)",
            ConditionalRules = EventFormatRules(),
            Constraints = CommonConstraints()
        },
        new()
        {
            ActivityType = "workshop",
            CanDraft = true,
            CanUpdate = true,
            RequiredFields = ["title", "description", "eventFormat", "activityDate", "activityUrl", "attendees"],
            OptionalFields = ["country", "inPersonAttendees", "tags", "additionalInfo", "private"],
            MetricField = "attendees (>= 1)",
            ConditionalRules = EventFormatRules(),
            Constraints = CommonConstraints()
        },
        new()
        {
            ActivityType = "mentoring",
            CanDraft = true,
            CanUpdate = true,
            RequiredFields = ["title", "description", "eventFormat", "activityDate", "activityUrl", "attendees"],
            OptionalFields = ["country", "inPersonAttendees", "tags", "additionalInfo", "private"],
            MetricField = "attendees (>= 1)",
            ConditionalRules = EventFormatRules(),
            Constraints = CommonConstraints()
        },
        new()
        {
            ActivityType = "product-feedback-given",
            CanDraft = true,
            CanUpdate = true,
            RequiredFields = ["title", "description", "contentType", "activityDate", "timeSpent"],
            OptionalFields = ["productDescription", "tags", "additionalInfo", "private"],
            MetricField = "timeSpent (minutes, >= 1)",
            ConditionalRules =
            [
                "contentType must be one of productFeedbackContentTypes (Early access program, Product feedback session).",
                "productDescription max length is 500 characters."
            ],
            Constraints = CommonConstraints()
        },
        new()
        {
            ActivityType = "interaction-with-googlers",
            CanDraft = true,
            CanUpdate = true,
            RequiredFields = ["title", "description", "format", "interactionType", "activityDate", "timeSpent"],
            OptionalFields = ["tags", "additionalInfo", "additionalLinks", "private"],
            MetricField = "timeSpent (minutes, >= 1)",
            ConditionalRules =
            [
                "format must be one of interactionFormats.",
                "interactionType must be one of interactionTypes.",
                "additionalLinks max length is 2000 characters."
            ],
            Constraints = CommonConstraints()
        },
        new()
        {
            ActivityType = "stories",
            CanDraft = true,
            CanUpdate = true,
            RequiredFields = ["title", "description", "whyIsSignificant", "significanceType", "activityUrl", "impact"],
            OptionalFields = ["tags", "additionalInfo", "private"],
            MetricField = "impact (>= 1)",
            ConditionalRules =
            [
                "significanceType must be one of significanceTypes.",
                "whyIsSignificant is required (max 2000 characters)."
            ],
            Constraints = CommonConstraints()
        },
        new()
        {
            ActivityType = "github-repository",
            CanDraft = false,
            CanUpdate = false,
            RequiredFields = [],
            OptionalFields = [],
            MetricField = "stars, forks, watching (read-only)",
            ConditionalRules = ["Read-only activity returned by GET /activities; cannot be drafted or patched."]
        },
        new()
        {
            ActivityType = "youtube-video",
            CanDraft = false,
            CanUpdate = false,
            RequiredFields = [],
            OptionalFields = [],
            MetricField = "views, likes, comments (read-only)",
            ConditionalRules = ["Read-only activity returned by GET /activities; cannot be drafted or patched."]
        }
    ];

    private static List<string> EventFormatRules() =>
    [
        "country is only accepted when eventFormat is In-Person or Hybrid.",
        "inPersonAttendees (>= 0) is only accepted when eventFormat is Hybrid.",
        "When eventFormat is Virtual, neither country nor inPersonAttendees may be provided."
    ];

    private static Dictionary<string, string> CommonConstraints() => new()
    {
        ["title"] = "3 to 200 characters",
        ["description"] = "max 2000 characters",
        ["additionalInfo"] = "max 2000 characters",
        ["activityDate"] = "YYYY-MM-DD (regex ^[0-9]{4}-[0-9]{2}-[0-9]{2}$)",
        ["activityUrl"] = "HTTP/HTTPS URL, max 500 characters",
        ["tags"] = "0 or more values from advocumcp://advocu/enums -> tags"
    };
}
