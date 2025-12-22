using System.Text.Json;
using System.Text.Json.Serialization;
using ModelContextProtocol;

namespace Advocu.Mcp.SSE;

[JsonSerializable(typeof(CreateContentCreationActivityRequest))]
[JsonSerializable(typeof(CreateInteractionWithGooglersActivityRequest))]
[JsonSerializable(typeof(CreateMentoringActivityRequest))]
[JsonSerializable(typeof(CreateProductFeedbackActivityRequest))]
[JsonSerializable(typeof(CreatePublicSpeakingActivityRequest))]
[JsonSerializable(typeof(CreateStoriesActivityRequest))]
[JsonSerializable(typeof(CreateWorkshopActivityRequest))]
[JsonSerializable(typeof(ActivityResponse))]
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
