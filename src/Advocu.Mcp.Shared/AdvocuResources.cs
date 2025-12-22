using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace Advocu.Mcp.Shared;

[McpServerResourceType]
public class AdvocuResources
{
    private readonly AdvocuApiClient _apiClient;

    public AdvocuResources(AdvocuApiClient apiClient)
    {
        _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
    }

    // [McpServerResource(UriTemplate = "advocumcp://advocu/tags", Name = "AdvocuTags", MimeType = "application/json")]
    // [Description("Get a list of allowed activity tags")]
    // public async Task<string> AllowedTags()
    // {
    //     var tags = await _apiClient.GetAllowedTags() ?? throw new Exception("No tags available");
    //     return JsonSerializer.Serialize(tags);
    // }
}