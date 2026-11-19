using System.Text.Json;
using System.Text.Json.Serialization;
using Advocu.Mcp.Shared;
using ModelContextProtocol;

namespace Advocu.Mcp.SSE;

/// <summary>
/// JSON serializer context for the Advocu MCP server.
/// </summary>
[JsonSerializable(typeof(CreateContentCreationActivityRequest))]
[JsonSerializable(typeof(CreateInteractionWithGooglersActivityRequest))]
[JsonSerializable(typeof(CreateMentoringActivityRequest))]
[JsonSerializable(typeof(CreateProductFeedbackActivityRequest))]
[JsonSerializable(typeof(CreatePublicSpeakingActivityRequest))]
[JsonSerializable(typeof(CreateStoriesActivityRequest))]
[JsonSerializable(typeof(CreateWorkshopActivityRequest))]
[JsonSerializable(typeof(ActivityResponse))]
[JsonSerializable(typeof(ActivitiesResponse))]
[JsonSerializable(typeof(ActivityItem))]
[JsonSerializable(typeof(PatchActivityRequest))]
[JsonSerializable(typeof(AdvocuEnumsSnapshot))]
[JsonSerializable(typeof(List<AdvocuActivitySchemaInfo>))]
[JsonSerializable(typeof(List<ActivitySummary>))]
[JsonSerializable(typeof(RecentActivitiesResult))]
[JsonSerializable(typeof(ActivitySearchResult))]
[JsonSerializable(typeof(DraftActivityResult))]
[JsonSerializable(typeof(UpdateActivityResult))]
internal sealed partial class AdvocuMcpContext : JsonSerializerContext
{
    public static JsonSerializerOptions DefaultOptions => CreateDefaultOptions();

    private static JsonSerializerOptions CreateDefaultOptions()
    {
        JsonSerializerOptions options = new(AdvocuMcpContext.Default.Options)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        options.TypeInfoResolverChain.Add(McpJsonUtilities.DefaultOptions.TypeInfoResolver!);
        options.MakeReadOnly();
        return options;
    }
}
