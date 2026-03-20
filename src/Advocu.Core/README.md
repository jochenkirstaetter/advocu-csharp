# Advocu.Core

This project is the core class library for the **Advocu C# SDK and Tooling**. It provides the foundational components required to interact with the Advocu Personal API across different client applications like the CLI, Web App, and Model Context Protocol (MCP) servers.

## Features
- **AdvocuApiClient**: A typed HTTP client that encapsulates authentication and API interactions for creating and managing advocacy activity drafts.
- **Data Models**: C# representations of Advocu activity requests and responses (e.g., `ActivityDraft`, `CreateContentCreationActivityRequest`, etc.).
- **Enums**: Strongly-typed enumerations mapping to Advocu API schemas (e.g., `AdvocuTag`, `AdvocuEventFormat`).
- **Mappers & Parsers**: Utilities for mapping local drafts to API requests and robust parsing of CLI/string inputs into Enums.
- **Settings & Commands**: Shared Spectre.Console command definitions and CLI settings required for scaffolding interactivity.

## Usage
The `AdvocuApiClient` requires an `IHttpClientFactory` and an `AdvocuApiSettings` configuration containing the necessary access token.
