using System.ComponentModel;
using ModelContextProtocol.Server;

namespace Advocu.Mcp.Shared;

[McpServerPromptType]
public class AdvocuPrompts
{
    [McpServerPrompt, Description("Draft a content creation activity")]
    public static string DraftContentCreationActivity()
    {
        return "Please draft a new content creation activity.";
    }

    [McpServerPrompt, Description("Get a list of allowed activity tags")]
    public static string AllowedTags()
    {
        return "Please provide a list of allowed tags for an activity.";
    }
}