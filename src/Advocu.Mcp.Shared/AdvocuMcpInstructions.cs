namespace Advocu.Mcp.Shared;

/// <summary>
/// Shared MCP server instructions guiding host models on how to use the Advocu MCP server effectively.
/// </summary>
public static class AdvocuMcpInstructions
{
    /// <summary>
    /// Gets the recommended workflow instructions exposed via MCP <c>ServerInstructions</c>.
    /// </summary>
    public const string Text = """
        You are connected to the Advocu MCP Server for managing Google Developer Experts (GDE) activities on Advocu.

        Recommended workflow:
        1. Discover allowed values first: read the `advocumcp://advocu/enums` resource (or call `get_enums`) before drafting or updating an activity. Never invent tag names, content types, or country names.
        2. Check field rules: read `advocumcp://advocu/schemas` when unsure which fields or metrics are required for a specific `activityType` (for example, `Hybrid` events require both `country` and `inPersonAttendees`).
        3. Avoid duplicates: inspect `advocumcp://advocu/activities/recent` or call `search_activities` before drafting a new activity.
        4. Validate before posting: call `draft_activity` with `dryRun: true` to validate all constraints and present a clear preview to the user; only submit with `dryRun: false` once confirmed.
        5. Partial updates: use `update_activity` with only the fields that changed. Note that `github-repository` and `youtube-video` activities are read-only and cannot be patched.
        """;
}
