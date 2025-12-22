# Gemini Project Context

## Project Overview

This project is a C# client for the Advocu Personal API. It allows users to programmatically create and post various types of activity drafts, such as content creation, public speaking, workshops, and more.

The project is built using .NET and utilizes the `Microsoft.Extensions.Hosting` and `Microsoft.Extensions.Http` libraries for configuration, dependency injection, and HTTP requests. It targets multiple .NET versions, including .NET 8, 9, and 10.

## Building and Running

To build and run this project, you will need the .NET SDK installed on your machine.

1.  **Configure the API client:**

    Open the `src/appsettings.json` file and add your Advocu API access token:

    ```json
    {
      "AdvocuApiSettings": {
        "AccessToken": "YOUR_ACCESS_TOKEN"
      }
    }
    ```

2.  **Build the solution or the project:**

    ```bash
    dotnet build Advocu.slnx
    ```

    Alternatively, build the specific project:

    ```bash
    dotnet build src/Advocu.Core/Advocu.Core.csproj
    ```

3.  **Run the project:**

    ```bash
    dotnet run --project src/Advocu.Core/Advocu.Core.csproj
    ```

## Development Conventions

*   **Dependency Injection:** The project uses the built-in .NET dependency injection framework to manage the `AdvocuApiClient` and its dependencies.
*   **Configuration:** Application settings are managed through the `appsettings.json` file and bound to the `AdvocuApiSettings` class.
*   **API Client:** The `AdvocuApiClient` class encapsulates all interactions with the Advocu API. It uses a typed `HttpClient` and handles authentication, JSON serialization, and error handling.
*   **Validation:** Client-side validation is performed using `System.ComponentModel.DataAnnotations` to ensure that data is valid before being sent to the API.
*   **Error Handling:** The API client includes error handling for HTTP request exceptions, including a specific check for rate limiting (HTTP 429).
*   **IDE:** The presence of a `.idea` directory suggests that the project is being developed using a JetBrains IDE, such as Rider or Visual Studio with ReSharper.
