using Advocu;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace Advocu.Mcp.Shared;

/// <summary>
/// Provides tools for the Advocu MCP server.
/// </summary>
[McpServerToolType]
public sealed class AdvocuTools
{
    private readonly AdvocuApiClient _apiClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdvocuTools"/> class.
    /// </summary>
    /// <param name="apiClient">The Advocu API client.</param>
    public AdvocuTools(AdvocuApiClient apiClient)
    {
        _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
    }

    /// <summary>
    /// Drafts a content creation activity.
    /// </summary>
    [McpServerTool(Name = "DraftContentCreationActivity"), Description("Draft a content creation activity")]
    public async Task<ActivityResponse> CreateContentCreationActivityAsync(CreateContentCreationActivityRequest request, CancellationToken cancellationToken = default)
    {
        return await _apiClient.PostContentCreationActivityAsync(request, cancellationToken);
    }

    /// <summary>
    /// Drafts a public speaking activity.
    /// </summary>
    [McpServerTool(Name = "DraftPublicSpeakingActivity"), Description("Draft a public speaking activity")]
    public async Task<ActivityResponse> CreatePublicSpeakingActivityAsync(CreatePublicSpeakingActivityRequest request, CancellationToken cancellationToken = default)
    {
        return await _apiClient.PostPublicSpeakingActivityAsync(request, cancellationToken);
    }

    /// <summary>
    /// Drafts a workshop activity.
    /// </summary>
    [McpServerTool(Name = "DraftWorkshopActivity"), Description("Draft a workshop activity")]
    public async Task<ActivityResponse> CreateWorkshopActivityAsync(CreateWorkshopActivityRequest request, CancellationToken cancellationToken = default)
    {
        return await _apiClient.PostWorkshopActivityAsync(request, cancellationToken);
    }

    /// <summary>
    /// Drafts a mentoring activity.
    /// </summary>
    [McpServerTool(Name = "DraftMentoringActivity"), Description("Draft a mentoring activity")]
    public async Task<ActivityResponse> CreateMentoringActivityAsync(CreateMentoringActivityRequest request, CancellationToken cancellationToken = default)
    {
        return await _apiClient.PostMentoringActivityAsync(request, cancellationToken);
    }

    /// <summary>
    /// Drafts a product feedback activity.
    /// </summary>
    [McpServerTool(Name = "DraftProductFeedbackActivity"), Description("Draft a product feedback activity")]
    public async Task<ActivityResponse> CreateProductFeedbackActivityAsync(CreateProductFeedbackActivityRequest request, CancellationToken cancellationToken = default)
    {
        return await _apiClient.PostProductFeedbackActivityAsync(request, cancellationToken);
    }

    /// <summary>
    /// Drafts an interaction with Googlers activity.
    /// </summary>
    [McpServerTool(Name = "DraftInteractionWithGooglerActivity"), Description("Draft an interaction with Googlers activity")]
    public async Task<ActivityResponse> CreateInteractionWithGooglersActivityAsync(CreateInteractionWithGooglersActivityRequest request, CancellationToken cancellationToken = default)
    {
        return await _apiClient.PostInteractionWithGooglersActivityAsync(request, cancellationToken);
    }

    /// <summary>
    /// Drafts a stories activity.
    /// </summary>
    [McpServerTool(Name = "DraftStoriesActivity"), Description("Draft a stories activity")]
    public async Task<ActivityResponse> CreateStoriesActivityAsync(CreateStoriesActivityRequest request, CancellationToken cancellationToken = default)
    {
        return await _apiClient.PostStoriesActivityAsync(request, cancellationToken);
    }

    /// <summary>
    /// Gets all Advocu API enumerations and their exact allowed values in one call.
    /// </summary>
    [McpServerTool(Name = "GetAdvocuEnums"), Description("Get all Advocu API enumerations (tags, content types, event formats, interaction formats, interaction types, significance types, activity types, and countries) in a single JSON response")]
    public AdvocuEnumsSnapshot GetAdvocuEnums()
    {
        return AdvocuEnumsSnapshot.Create();
    }

    /// <summary>
    /// Retrieves a paginated list of submitted activities.
    /// </summary>
    [McpServerTool(Name = "GetActivities"), Description("Get a paginated list of Advocu activities sorted by submission date ascending (GET /activities)")]
    public async Task<ActivitiesResponse> GetActivitiesAsync(
        [Description("Optional inclusive lower bound for submission date (ISO-8601 UTC, e.g. 2026-01-01T00:00:00Z)")] DateTimeOffset? from = null,
        [Description("Optional exclusive upper bound for submission date (ISO-8601 UTC, e.g. 2026-01-01T00:00:00Z)")] DateTimeOffset? to = null,
        [Description("Optional lower bound for activities updated after this timestamp (ISO-8601 UTC)")] DateTimeOffset? updatedAfter = null,
        [Description("Optional zero-based page number (default: 0)")] int? page = null,
        [Description("Optional page size from 1 to 100 (default: 10)")] int? size = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetActivitiesQuery
        {
            From = from,
            To = to,
            UpdatedAfter = updatedAfter,
            Page = page,
            Size = size
        };

        return await _apiClient.GetActivitiesAsync(query, cancellationToken);
    }

    /// <summary>
    /// Partially updates an existing activity.
    /// </summary>
    [McpServerTool(Name = "PatchActivity"), Description("Partially update an existing Advocu activity by activityId (PATCH /activities/{activityId})")]
    public async Task<ActivityItem> PatchActivityAsync(
        [Description("The activityId of the activity to update")] string activityId,
        [Description("The partial activity update payload")] PatchActivityRequest request,
        CancellationToken cancellationToken = default)
    {
        return await _apiClient.PatchActivityAsync(activityId, request, cancellationToken);
    }
}
