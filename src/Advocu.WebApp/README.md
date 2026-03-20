# Advocu.WebApp

A modern, interactive **Blazor Server** web application serving as a graphical front-end for the Advocu C# SDK context.

## Purpose
This application allows advocates, developers, and speakers to manage and log their advocacy activities completely inside a user-friendly browser interface without needing the command line.

## Features
- **Interactive Forms**: User interfaces for Content Creation, Stories, Workshop generation, Public Speaking logging, Mentoring sessions, and Interaction inputs.
- **Blazor Server Integration**: High-performance streaming of data directly utilizing `AdvocuApiClient`.
- **Razor Components**: Well-architected reusable shared components (such as generic `<MultiSelect />` structures for managing activity tags).

## Setup & Running
Configure your environment variable or application settings JSON (`AdvocuApiSettings:AccessToken`) before execution.

```bash
dotnet run --project src/Advocu.WebApp
```
The console will display the local host URL where you can view the application in your standard browser.
