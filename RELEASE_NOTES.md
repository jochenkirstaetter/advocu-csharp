# Release Notes 0.6.0

## New Endpoints, Enum Sync, and .NET 10 / .NET 11 Support

### New Personal API Endpoints
- Added `GET /activities` (`GetActivitiesAsync`) and `PATCH /activities/{activityId}` (`PatchActivityAsync`) across `Advocu.Core`, the CLI (`advocu activities`, `advocu patch`), and the MCP server (`GetActivities`, `PatchActivity`).

### Consolidated Enumerations JSON
- Added `AdvocuEnumsSnapshot` so all Advocu API enumerations can be fetched in a single JSON payload via the `advocumcp://advocu/enums` MCP resource, the `GetAdvocuEnums` MCP tool, or `advocu list all`.

### Synced Enums, Validation & Dependencies
- Synced `AdvocuTag` (86 active tags) and `AdvocuActivityContentType` with the live Advocu Personal API.
- Aligned `POST /activity-drafts/*` client-side validation with current API length limits, regex patterns, and content-type rules.
- Upgraded all NuGet dependencies and updated all projects to multi-target `net10.0;net11.0`.
