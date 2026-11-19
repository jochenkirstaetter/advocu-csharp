# Advocu.McpServer

This project is a **Model Context Protocol (MCP)** server for the Advocu Personal API, using **Standard I/O (stdio)** transport (an HTTP/SSE host is also available in `Advocu.Mcp.SSE`).

## Capabilities

- **Server Instructions:** Advertises recommended workflows via `ServerInstructions` (read enums, check schemas and recent activities for duplicates, validate with `dryRun: true` before submitting).
- **Resources (`application/json`):**
  - `advocumcp://advocu/enums` - All allowed API enumerations in one JSON payload.
  - `advocumcp://advocu/schemas` - Field rules, length limits, regex patterns, metrics, and conditional constraints for all 9 activity types.
  - `advocumcp://advocu/activities/recent` - Compact summaries of recent submitted activities.
  - `advocumcp://advocu/activities/{activityId}` - Resource template for single-activity inspection.
- **Prompts:** `draft_activity_guided`, `update_activity_guided`, `impact_report`, and `batch_draft_activities`.
- **Tools:**
  - `get_enums` (`ReadOnly`, `Idempotent`)
  - `search_activities` (`ReadOnly`, `Idempotent`)
  - `draft_activity` (unified across all 7 draftable types, with fuzzy enum/tag matching and `dryRun: true` validation)
  - `update_activity` (`Idempotent`, partial updates with fuzzy enum/tag matching and `dryRun: true` validation)

## Usage

Configure the server as a local stdio MCP server in your MCP host (for example Antigravity, Claude Desktop, or VS Code):

```bash
dotnet run --project src/Advocu.McpServer
```

Set `ADVOCU_API_TOKEN` in the server's environment variables (and optionally `ADVOCU_URL` to override the default Personal API base URL).
