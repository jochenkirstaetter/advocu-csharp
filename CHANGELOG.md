# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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
