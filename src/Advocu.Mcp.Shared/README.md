# Advocu.Mcp.Shared

A shared library providing the Model Context Protocol (MCP) definitions for the Advocu integration. This library acts as the bridge between large language models (LLMs) and the Advocu API by defining precise, toolable structures.

## Features
- **MCP Tools**: Exposes Advocu API capabilities (like `DraftContentCreationActivity`, `DraftPublicSpeakingActivity`, etc.) as well-defined model tools via the `[McpServerToolType]` attribute.
- **MCP Prompts**: Includes customized AI prompts (e.g., templates for scaffolding a new draft) to help standard usage of tools.
- **MCP Resources**: A framework for injecting resources (such as dynamic tag lists or enum constraints) directly into the AI's context window.

This project is intended to be referenced by specific MCP Server implementations (such as Stdio or SSE servers) to provide the actual tool resolutions.
