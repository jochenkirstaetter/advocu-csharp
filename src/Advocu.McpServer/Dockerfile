# Use the SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy csproj and restore
COPY src/Advocu.McpServer/Advocu.McpServer.csproj src/Advocu.McpServer/
COPY src/Advocu.Mcp.Shared/Advocu.Mcp.Shared.csproj src/Advocu.Mcp.Shared/
COPY src/Advocu.Core/Advocu.Core.csproj src/Advocu.Core/

RUN dotnet restore src/Advocu.McpServer/Advocu.McpServer.csproj

# Copy everything else and build
COPY src/Advocu.McpServer/. src/Advocu.McpServer/
COPY src/Advocu.Mcp.Shared/. src/Advocu.Mcp.Shared/
COPY src/Advocu.Core/. src/Advocu.Core/
WORKDIR /app/src/Advocu.McpServer
RUN dotnet publish -c Release -o /app/out -f net10.0

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app/out .

# Set environment variable placeholder (user should override this)
ENV ADVOCU_API_TOKEN=""

ENTRYPOINT ["dotnet", "Advocu.McpServer.dll"]
