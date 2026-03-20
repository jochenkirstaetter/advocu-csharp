# Advocu.Mcp.SSE

This project is an **HTTP/SSE Model Context Protocol (MCP)** server for Advocu. 

## Purpose
It provides Server-Sent Events (SSE) integration allowing web-based and distributed language models, or specific browser/cloud IDE MCP clients, to communicate with the Advocu API via standardized HTTP mechanisms.

## Features
- Runs as an ASP.NET Core web application, providing standard endpoint integration.
- Resolves HTTP clients and streams context/messages seamlessly through HTTP channels.

## Usage
You can run this project locally, typically binding to an HTTP port (e.g., `http://localhost:5000`). Clients can then access tools and resources by registering the web endpoints as opposed to a local terminal command.

```bash
dotnet run --project src/Advocu.Mcp.SSE
```
Ensure you have configured your `ADVOCU_API_TOKEN` via environment variables.
