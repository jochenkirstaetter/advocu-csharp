using Advocu;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace Advocu.Mcp.Shared;

[McpServerToolType]
public sealed class AdvocuTools
{
    private readonly AdvocuApiClient _apiClient;

    public AdvocuTools(AdvocuApiClient apiClient)
    {
        _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
    }

    [McpServerTool(Name = "DraftContentCreationActivity"), Description("Draft a content creation activity")]
    public async Task<ActivityResponse> CreateContentCreationActivityAsync(CreateContentCreationActivityRequest request, CancellationToken cancellationToken = default)
    {
        return await _apiClient.PostContentCreationActivityAsync(request, cancellationToken);
    }

    [McpServerTool(Name = "DraftPublicSpeakingActivity"), Description("Draft a public speaking activity")]
    public async Task<ActivityResponse> CreatePublicSpeakingActivityAsync(CreatePublicSpeakingActivityRequest request, CancellationToken cancellationToken = default)
    {
        return await _apiClient.PostPublicSpeakingActivityAsync(request, cancellationToken);
    }

    [McpServerTool(Name = "DraftWorkshopActivity"), Description("Draft a workshop activity")]
    public async Task<ActivityResponse> CreateWorkshopActivityAsync(CreateWorkshopActivityRequest request, CancellationToken cancellationToken = default)
    {
        return await _apiClient.PostWorkshopActivityAsync(request, cancellationToken);
    }

    [McpServerTool(Name = "DraftMentoringActivity"), Description("Draft a mentoring activity")]
    public async Task<ActivityResponse> CreateMentoringActivityAsync(CreateMentoringActivityRequest request, CancellationToken cancellationToken = default)
    {
        return await _apiClient.PostMentoringActivityAsync(request, cancellationToken);
    }

    [McpServerTool(Name = "DraftProductFeedbackActivity"), Description("Draft a product feedback activity")]
    public async Task<ActivityResponse> CreateProductFeedbackActivityAsync(CreateProductFeedbackActivityRequest request, CancellationToken cancellationToken = default)
    {
        return await _apiClient.PostProductFeedbackActivityAsync(request, cancellationToken);
    }

    [McpServerTool(Name = "DraftInteractionWithGooglerActivity"), Description("Draft an interaction with Googlers activity")]
    public async Task<ActivityResponse> CreateInteractionWithGooglersActivityAsync(CreateInteractionWithGooglersActivityRequest request, CancellationToken cancellationToken = default)
    {
        return await _apiClient.PostInteractionWithGooglersActivityAsync(request, cancellationToken);
    }

    [McpServerTool(Name = "DraftStoriesActivity"), Description("Draft a stories activity")]
    public async Task<ActivityResponse> CreateStoriesActivityAsync(CreateStoriesActivityRequest request, CancellationToken cancellationToken = default)
    {
        return await _apiClient.PostStoriesActivityAsync(request, cancellationToken);
    }
}
