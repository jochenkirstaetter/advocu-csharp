# Release Notes 0.7.0

## MCP-Native Server Redesign (4 Resources, 4 Prompts, 4 Tools)

We rebuilt the Advocu MCP server (`Advocu.Mcp.Shared`, `Advocu.McpServer`, and `Advocu.Mcp.SSE`) from scratch around native Model Context Protocol capabilities instead of thin 1:1 REST wrappers:

### Server Instructions
- Both stdio (`Advocu.McpServer`) and HTTP/SSE (`Advocu.Mcp.SSE`) hosts now advertise `ServerInstructions` guiding MCP clients through enum discovery, duplicate checks, dry-run validation, and safe partial updates.

### Resources
- `advocumcp://advocu/enums` - All allowed API enumerations in one JSON document.
- `advocumcp://advocu/schemas` - Field rules, length limits, regexes, metrics, and conditional constraints for all 9 activity types.
- `advocumcp://advocu/activities/recent` - Compact summaries of recent activities for duplicate checking.
- `advocumcp://advocu/activities/{activityId}` - Resource template for single-activity lookup.

### Parameterized Workflow Prompts
- `draft_activity_guided`, `update_activity_guided`, `impact_report`, and `batch_draft_activities`.

### 4 Annotated Workflow Tools
- `get_enums` (`ReadOnly`, `Idempotent`) - Tool fallback for hosts that do not read resources directly.
- `search_activities` (`ReadOnly`, `Idempotent`) - Date, type, tag, and keyword filtering with compact summary output by default.
- `draft_activity` - Unified draft tool across all 7 draftable activity types with fuzzy enum/tag matching and `dryRun: true` validation.
- `update_activity` (`Idempotent`) - Partial update tool with fuzzy enum/tag matching and `dryRun: true` validation.
