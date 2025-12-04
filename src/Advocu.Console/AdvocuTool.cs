using System.ComponentModel;
using ModelContextProtocol.Server;

namespace Advocu;

[McpServerToolType]
public sealed class AdvocuTool
{
    [McpServerTool, Description("")]
    public static async Task<string> DraftContentCreation(
        HttpClient client,
        [Description("")] string information)
    {
        var advocuClient = new AdvocuApiClient(httpClient: client, "");
        var response = await advocuClient.PostContentCreationActivityAsync(
            new CreateContentCreationActivityRequest
            {
                Title = "",
                Description = ""
            });

        return response.Id;
    }
}