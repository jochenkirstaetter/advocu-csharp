using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace Advocu.Mcp.Shared;

/// <summary>
/// Defines resources for the Advocu MCP server.
/// </summary>
[McpServerResourceType]
public class AdvocuResources
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true
    };

    /// <summary>
    /// Returns all Advocu API enumerations and their exact allowed values in a single JSON document.
    /// </summary>
    [McpServerResource(UriTemplate = "advocumcp://advocu/enums", Name = "AdvocuEnums", MimeType = "application/json")]
    [Description("Get all Advocu API enumerations (tags, content types, event formats, interaction formats, interaction types, significance types, activity types, and countries) in a single JSON payload.")]
    public static string GetAllEnums()
    {
        return JsonSerializer.Serialize(AdvocuEnumsSnapshot.Create(), SerializerOptions);
    }
}
