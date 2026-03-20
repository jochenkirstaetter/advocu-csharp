# Advocu.McpServer

This project is a **Model Context Protocol (MCP)** server for the Advocu platform, utilizing **Standard I/O (stdio)** for message transport. 

## Purpose
The standard I/O MCP server bridges standard, local AI assistants and LLM tools (like the Anthropic Desktop App, Claude CLI, or other desktop-based AI environments) with the Advocu API. It establishes secure and performant communication over standard streams.

## Features
- Transports generic JSON-RPC messages from MCP clients into strongly-typed `Advocu.Mcp.Shared` tool invocations.
- Easily connects AI environments directly to an `ADVOCU_API_TOKEN` environment, making requests securely.
- Streamlined configuration via `Microsoft.Extensions.Hosting`.

## Usage
Add this server as a Local Application/Command via your LLM's MCP settings using standard arguments, for example configuring the command as `dotnet run --project src/Advocu.McpServer`. Make sure `ADVOCU_API_TOKEN` is present in your environment variables.
