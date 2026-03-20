using System.ComponentModel;
using ModelContextProtocol.Server;

namespace Advocu;

/// <summary>
/// Provides MCP server tools related to Advocu functionality.
/// </summary>
/// <remarks>
/// This class registers functions that can be invoked via the Model Context Protocol.
/// </remarks>
[McpServerToolType]
public sealed class AdvocuTool
{
    /// <summary>
    /// Creates a draft for a content creation activity.
    /// </summary>
    /// <param name="client">The HTTP client to use for the API request.</param>
    /// <param name="information">Information about the content to be created.</param>
    /// <returns>The ID of the created draft.</returns>
    /// <remarks>
    /// This method is registered as an MCP tool to automate draft creation for content.
    /// </remarks>
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
                Description = "",
                ActivityDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ActivityUrl = ""
            });

        return response.Id;
    }
}