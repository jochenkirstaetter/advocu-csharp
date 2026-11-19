# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.6.0] - 2026-11-19

### Added
- Support for `GET /activities` (`GetActivitiesAsync`) and `PATCH /activities/{activityId}` (`PatchActivityAsync`) in `Advocu.Core`, the CLI (`advocu activities`, `advocu patch`), and the MCP server (`GetActivities`, `PatchActivity`).
- Consolidated enumerations snapshot (`AdvocuEnumsSnapshot`) exposed as an MCP resource (`advocumcp://advocu/enums`), MCP tool (`GetAdvocuEnums`), and CLI command (`advocu list all`).
- Multi-targeting for `.NET 10` and `.NET 11` (`net10.0;net11.0`) across all projects.

### Changed
- Upgraded NuGet packages to latest versions (`Microsoft.Extensions.*` and `System.Text.Json` `11.0.0`, `ModelContextProtocol` `2.4.0`, `OpenTelemetry.*` `1.21.0`, `Spectre.Console.Cli` `0.57.2`).
- Synced `AdvocuTag` (86 active tags) and `AdvocuActivityContentType` (9 content types) with the live Advocu Personal API.
- Tightened `POST /activity-drafts/*` request validation (`Title` max length 200, `ActivityDate` and `ActivityUrl` regex patterns, `City`/`CityPlaceId` length bounds, `WhyIsSignificant` required, and `ContentType` cross-type validation).
- Dropped `net8.0` and `net9.0` target frameworks.

## [0.5.1] - 2026-08-14

### Changed
- Updated CI/CD workflow.

## [0.5.0] - 2026-08-14

### Changed
- Converted solution to Central Package Management (CPM) with `Directory.Packages.props`.
- Moved NuGet publishing to NuGet Trusted Publishing (OIDC) and enhanced CI/CD changelog generation.
- Upgraded NuGet packages.

## [0.4.0] - 2026-03-20

### Added
- Initialize `ActivityDate` and `ActivityUrl` fields in various activity creation requests to prevent nullable property errors when using the library programmatically.

### Changed
- Project namespaces updated from `Advocu.NuGet` to `Advocu.Core`.
- Improved activity request model validation.

## [0.3.0] - 2026-01-12

### Added
- **Detailed Activity Review**: The `interactive` command now shows command-specific details in the "Review Draft" summary before submission.

### Fixed
- **Multiple Tags**: Corrected the handling of multiple tags selection in the interactive draft creation.

## [0.2.0] - 2026-01-02

### Added
- **Interactive Mode**: New `interactive` command providing a wizard-like experience for creating activity drafts.
- **Persistent Drafts**: Drafts are now saved to `~/.advocu/drafts.json` and can be resumed across CLI sessions.
- **Token Management**: Persistent storage for API tokens in `~/.config/advocu/token.json` (or `%APPDATA%`).
- **Unified Token Prompt**: The CLI automatically prompts for an API token if one is not provided via arguments or environment variables.

### Changed
- Refactored `AdvocuSettings` to prioritize token resolution: CLI Flag > Environment Variable > Persisted File > Interactive Prompt.
- Updated all activity commands to use the central token resolution logic.

## [0.1.0] - initial release

### Added
- Initial release of Advocu C# Client Library and CLI.
- Support for Content Creation, Public Speaking, Workshop, Mentoring, Product Feedback, Googler Interaction, and Stories activities.
- `AdvocuApiClient` for programmatic access to the Advocu Personal API.
