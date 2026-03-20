# Advocu.Console

A powerful command-line interface (CLI) built with **Spectre.Console** to interact with the Advocu Personal API directly from your terminal.

## Features
- **Interactive Drafting**: Create complex activity drafts through an intuitive wizard-like terminal experience.
- **Support for All Activity Types**: Supports Content Creation, Public Speaking, Workshops, Mentoring, Product Feedback, Interactions with Googlers, and Stories.
- **Local Draft Persistence**: Drafts are saved locally so you can resume them if you step away.
- **Direct Submission**: Allows immediate conversion of local drafts and command-line inputs into actual Advocu drafts via the `AdvocuApiClient`.
- **Enum Listings**: Easily list available tags, supported countries, event formats, or interaction types directly from the command line.

## Usage
Run the CLI using standard `.NET` tools. You can provide your API token via the `--api-token` flag, an `.env` file, or as a stored profile setting from interactive prompts.

```bash
# Start the interactive workflow
dotnet run --project src/Advocu.Console -- interactive

# Or run specific commands (e.g., list tags)
dotnet run --project src/Advocu.Console -- list tags
```
