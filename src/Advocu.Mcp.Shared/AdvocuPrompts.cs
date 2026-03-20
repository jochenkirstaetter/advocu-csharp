using System.ComponentModel;
using ModelContextProtocol.Server;

namespace Advocu.Mcp.Shared;

/// <summary>
/// Defines prompts for the Advocu MCP server.
/// </summary>
[McpServerPromptType]
public class AdvocuPrompts
{
    /// <summary>
    /// Returns a prompt for drafting a content creation activity.
    /// </summary>
    [McpServerPrompt, Description("Draft a content creation activity")]
    public static string DraftContentCreationActivity()
    {
        return "Please draft a new content creation activity.";
    }

    /// <summary>
    /// Returns a prompt for retrieving a list of allowed tags.
    /// </summary>
    [McpServerPrompt, Description("Get a list of allowed activity tags")]
    public static string AllowedTags()
    {
        return "Please provide a list of allowed tags for an activity.";
    }
}