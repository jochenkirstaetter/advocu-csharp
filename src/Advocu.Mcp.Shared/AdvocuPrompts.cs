using System.ComponentModel;
using ModelContextProtocol.Server;

namespace Advocu.Mcp.Shared;

/// <summary>
/// Defines reusable, workflow-driven MCP prompt templates for common Advocu GDE tasks.
/// </summary>
[McpServerPromptType]
public sealed class AdvocuPrompts
{
    /// <summary>
    /// Guides the model through turning raw notes or a URL into a validated Advocu activity draft.
    /// </summary>
    [McpServerPrompt(Name = "draft_activity_guided")]
    [Description("Guided workflow to classify raw notes or a URL, map valid Advocu tags, check for duplicates, preview, and draft an activity.")]
    public static string DraftActivityGuided(
        [Description("Raw notes, description, event details, or URL describing the activity.")] string notes,
        [Description("Optional activity type slug (content-creation, public-speaking, workshop, mentoring, product-feedback-given, interaction-with-googlers, stories).")] string? activityType = null)
    {
        var typeHint = string.IsNullOrWhiteSpace(activityType)
            ? "Determine the most appropriate activityType slug from `advocumcp://advocu/schemas`."
            : $"Use activityType `{activityType}` and verify its required fields in `advocumcp://advocu/schemas`.";

        return $"""
            Help me draft a new Advocu GDE activity from the following input:
            {notes}

            Follow these steps:
            1. {typeHint}
            2. Read `advocumcp://advocu/enums` (or call `get_enums`) and select only exact valid values for tags, contentType, eventFormat, format, interactionType, significanceType, and country.
            3. Check `search_activities` or `advocumcp://advocu/activities/recent` to make sure this activity has not already been logged.
            4. If any required field (such as attendees, readers, timeSpent, activityDate, activityUrl, or country for In-Person/Hybrid events) is missing, ask me briefly before submitting.
            5. Call `draft_activity` with `dryRun: true` to validate the payload, show me a concise summary, and wait for my confirmation before calling `draft_activity` with `dryRun: false`.
            """;
    }

    /// <summary>
    /// Guides the model through finding an existing activity and applying a safe partial update.
    /// </summary>
    [McpServerPrompt(Name = "update_activity_guided")]
    [Description("Guided workflow to locate an existing Advocu activity, review current vs. proposed values, and apply a partial update.")]
    public static string UpdateActivityGuided(
        [Description("Search keywords, title, or activityId to locate the activity.")] string query,
        [Description("Description of the changes to apply.")] string changes)
    {
        return $"""
            Help me update an existing Advocu activity.
            - Target hint: {query}
            - Requested changes: {changes}

            Follow these steps:
            1. Call `search_activities` (using keyword or date filters) to find the matching activity and confirm it is editable (not `github-repository` or `youtube-video`).
            2. Read `advocumcp://advocu/enums` if any tags or enum fields are being modified.
            3. Show me the current values vs. the proposed partial update (only changed fields).
            4. Call `update_activity` with `dryRun: true` first to validate, and apply with `dryRun: false` once I confirm.
            """;
    }

    /// <summary>
    /// Guides the model through generating a structured GDE impact report over a date range.
    /// </summary>
    [McpServerPrompt(Name = "impact_report")]
    [Description("Generate a structured GDE impact summary (counts by type, total readers, attendees, time spent, impact, and top tags) for a date range.")]
    public static string ImpactReport(
        [Description("Inclusive start timestamp in ISO-8601 UTC format (e.g. 2026-01-01T00:00:00Z).")] string from,
        [Description("Exclusive end timestamp in ISO-8601 UTC format (e.g. 2026-04-01T00:00:00Z).")] string to)
    {
        return $"""
            Generate a GDE impact report for my Advocu activities from {from} to {to}.

            Follow these steps:
            1. Call `search_activities` with `from: "{from}"`, `to: "{to}"`, and `summaryOnly: false` (paginate if needed to collect all activities in the range).
            2. Summarize:
               - Total activities and breakdown by activity type.
               - Aggregate metrics: total readers, total attendees (and in-person attendees), total time spent (minutes/hours), total story impact, GitHub stars/forks, and YouTube views/likes.
               - Top tags used across the period.
               - A concise highlights list of the highest-impact activities.
            """;
    }

    /// <summary>
    /// Guides the model through extracting multiple activity drafts from bulk notes.
    /// </summary>
    [McpServerPrompt(Name = "batch_draft_activities")]
    [Description("Extract multiple Advocu activity drafts from raw notes (such as a conference trip or monthly log), validate each, and submit after confirmation.")]
    public static string BatchDraftActivities(
        [Description("Raw notes containing multiple activities to extract and draft.")] string notes)
    {
        return $"""
            Extract all distinct Advocu GDE activities from the following notes and prepare drafts for each:
            {notes}

            Follow these steps:
            1. Read `advocumcp://advocu/enums` and `advocumcp://advocu/schemas`.
            2. Check `search_activities` for the relevant dates to skip any activities already logged.
            3. For each new activity, call `draft_activity` with `dryRun: true` to validate all fields and tags.
            4. Present a table of all validated drafts (and flag any missing required details).
            5. Only submit with `dryRun: false` for the items I approve.
            """;
    }
}
