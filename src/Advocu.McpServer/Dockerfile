ARG DOTNET_VERSION=11.0

# Use the SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
ARG DOTNET_VERSION=11.0
WORKDIR /app

# Copy props and csproj and restore
COPY Directory.Build.props Directory.Packages.props ./
COPY src/Advocu.McpServer/Advocu.McpServer.csproj src/Advocu.McpServer/
COPY src/Advocu.Mcp.Shared/Advocu.Mcp.Shared.csproj src/Advocu.Mcp.Shared/
COPY src/Advocu.Core/Advocu.Core.csproj src/Advocu.Core/

RUN dotnet restore src/Advocu.McpServer/Advocu.McpServer.csproj -p:TargetFrameworks=net${DOTNET_VERSION}

# Copy everything else and build
COPY src/Advocu.McpServer/. src/Advocu.McpServer/
COPY src/Advocu.Mcp.Shared/. src/Advocu.Mcp.Shared/
COPY src/Advocu.Core/. src/Advocu.Core/
WORKDIR /app/src/Advocu.McpServer
RUN dotnet publish -c Release -o /app/out -f net${DOTNET_VERSION} -p:TargetFrameworks=net${DOTNET_VERSION}

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:${DOTNET_VERSION}
WORKDIR /app
COPY --from=build /app/out .

# Set environment variable placeholder (user should override this)
ENV ADVOCU_API_TOKEN=""

ENTRYPOINT ["dotnet", "Advocu.McpServer.dll"]
