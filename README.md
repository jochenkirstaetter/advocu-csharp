# Advocu C#

An Advocu client for .NET

👋 Hello Google Developer Experts! 

Reporting your awesome community work shouldn't be a chore. Whether you want to integrate reporting directly into your own .NET applications or prefer a quick command-line tool to submit your activities, we've got you covered. This library provides a friendly C# client for the Advocu Personal API and a powerful CLI tool to streamline your GDE reporting activities.

## 🚀 Getting Started

Get your Advocu Personal API token from [Advocu](https://app.advocu.com/settings/integrations/personal-api) and store it in an environment variable `ADVOCU_API_TOKEN`. Alternatively, you can pass it as a command-line argument `--api-token` to the CLI tool.

### Ad hoc without installation (.NET 10 / .NET 11)

Run the `dnx` command to use the Advocu CLI tool:

```bash
dnx advocu -y
```

Or pass arguments directly using the double-dash `--`:

```bash
dnx advocu -y -- content -t "Creating Advocu C# using Antigravity" -d "This blog article describes..." ...
```

Or use the interactive mode:

```bash
dnx advocu -y -- interactive
```

### CLI Tool (.NET 10 / .NET 11)

Install the `advocu` global tool to use it from anywhere:

```bash
dotnet tool install --global Advocu
```

### Library (.NET 10 / .NET 11)

Add the client library to your .NET project:

```bash
dotnet add package Advocu
```

---

## 📚 Using the `advocu` CLI Tool

```
Advocu C#
An Advocu client for .NET

Reporting your awesome community work shouldn't be a chore. Whether you want to integrate reporting directly into your own .NET applications or prefer a quick command-line tool to submit your activities, we've got you covered. This library provides a friendly C# client for the Advocu Personal API and a powerful CLI tool to streamline your GDE reporting activities.

USAGE:
    advocu [OPTIONS] [COMMAND]

OPTIONS:
    -h, --help                 Prints help information                                                
    -a, --api-token <TOKEN>    The Advocu API Token. Defaults to ADVOCU_API_TOKEN environment variable
    -u, --api-url <URL>        The Advocu API URL. Defaults to ADVOCU_URL environment variable        
    -p, --private              Mark the activity as private                                           

COMMANDS:
    interactive    Start the interactive wizard mode (New! ✨)
    content        Creates a new Content Creation Activity draft
    public         Creates a new Public Speaking Activity draft
    workshop       Creates a new Workshop Activity draft
    mentoring      Creates a new Mentoring Activity draft
    feedback       Creates a new Product Feedback Activity draft
    interaction    Creates a new Interaction with Googlers Activity draft
    stories        Creates a new Stories Activity draft
    activities     Retrieves submitted activities (GET /activities)
    patch          Partially updates an existing activity (PATCH /activities/{activityId})
    list           Lists available options for enumerations (including `list all` JSON output)
```

Each command has their own help information and provides details regarding the activity details.

---

## 💻 Using the `AdvocuApiClient`

The `AdvocuApiClient` is the heart of the library, designed to make interacting with the Advocu API smooth and type-safe.

### Setup using Dependency Injection

In your `Program.cs` or startup configuration, you can easily register the client with your service collection:

```csharp
using Advocu;

var builder = Host.CreateApplicationBuilder(args);

// Register the Advocu Client
builder.Services.AddHttpClient<AdvocuApiClient>((provider, client) =>
{
    // You can load these from your configuration
    var accessToken = provider.GetRequiredService<IConfiguration>()["Advocu:AccessToken"];
    client.BaseAddress = new Uri("https://api.advocu.com/personal-api/v1/gde/");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
});

var host = builder.Build();
```

### Example: Reporting a Workshop

Here's how you can create and submit a draft for a workshop you just hosted:

```csharp
using Advocu;

public async Task ReportWorkshop(AdvocuApiClient client)
{
    var workshopRequest = new CreateWorkshopActivityRequest
    {
        Title = "Gemini CLI Magic",
        Description = "A hands-on workshop demonstrating the power of Gemini CLI for developers.",
        ActivityDate = "2025-11-29",
        EventFormat = AdvocuEventFormat.Virtual,
        Country = AdvocuCountry.Uzbekistan,
        Attendees = 150,
        ActivityUrl = "https://gdg.community.dev/events/...",
        Tags = [AdvocuTag.AiGemini, AdvocuTag.BuildWithAi, AdvocuTag.GoogleCloud]
    };

    try
    {
        var response = await client.PostWorkshopActivityAsync(workshopRequest);
        Console.WriteLine($"🎉 Draft created! ID: {response.Id}, Status: {response.Status}");
    }
    catch (ValidationException ex)
    {
        Console.WriteLine($"Oops! details missing: {ex.Message}");
    }
}
```

### Example: Listing and Updating Activities

You can query submitted activities (`GET /activities`), partially update an editable activity (`PATCH /activities/{activityId}`), or retrieve all API enumerations in a single object:

```csharp
using Advocu;

// Retrieve paginated activities
var activities = await client.GetActivitiesAsync(new GetActivitiesQuery
{
    From = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
    Page = 0,
    Size = 20
});

// Partially update an existing activity
var updated = await client.PatchActivityAsync("activity-id", new PatchActivityRequest
{
    Data = new PatchActivityData
    {
        Title = "Updated Workshop Title",
        Tags = ["AI", "AI - Gemini", "Google Cloud"]
    }
});

// Get all API enumerations and their exact values in one object
var enums = AdvocuEnumsSnapshot.Create();
```

Check out the `Advocu.Console` and `Advocu.WebApp` projects in this repository for more complete examples!

---

## 🛠️ The `advocu` CLI

Prefer the terminal? The `advocu` CLI allows you to draft activities without leaving your command line. Perfect for scripting or quick reports!

### Configuration

The CLI supports a robust token resolution strategy in the following order:
1.  **Command Line Flag**: `--api-token <TOKEN>`
2.  **Environment Variable**: `ADVOCU_API_TOKEN`
3.  **Persisted File**: `~/.config/advocu/token.json` (Linux) or `%APPDATA%/advocu/token.json` (Windows)
4.  **Interactive Prompt**: If no token is found, the CLI will prompt you securely and save it for future use.

You can also set the API URL via `-u` or `ADVOCU_URL`.

### Interactive Mode 🧙‍♂️
The easiest way to use the CLI is the new interactive mode. It guides you through the creation process, supports rich selection menus, and **saves your progress** automatically so you can resume later.

```bash
advocu interactive
```

Prefer manual commands? No problem!

### Workshops 🎓
Hosted a workshop? Draft it in seconds:

```bash
advocu workshop \
  --title "Deep Dive into .NET 10" \
  --description "Advanced workshop on the new features of .NET 10 and C# 14." \
  --date 2025-12-01 \
  --country "Mauritius" \
  --format InPerson \
  --attendees 50 \
  --tags "DotNet" "CloudAppDevelopment" \
  --url "https://meetup.com/..."
```

### Mentorship 🤝
Helping other developers grow? Log your mentoring sessions easily:

```bash
advocu mentoring \
  --title "GDE Mentoring Session" \
  --description "One-on-one session helping a startup founder with Google Cloud architecture." \
  --date 2025-12-05 \
  --format Virtual \
  --attendees 1 \
  --tags "GoogleCloud" "Startup"
```

### Listing & Updating Activities

Retrieve submitted activities with optional date filters and pagination:

```bash
advocu activities --from 2026-01-01T00:00:00Z --page 0 --size 20
advocu activities --updated-after 2026-06-01T00:00:00Z --json
```

Partially update an existing activity by ID:

```bash
advocu patch <ACTIVITY_ID> --title "Updated Title" --tags "AI,AI - Gemini,Google Cloud" --set-private false
```

Output all enumerations and their exact API values as a single JSON document:

```bash
advocu list all
```

### Available Commands
- `content` - Articles, videos, etc.
- `public` - Public speaking.
- `workshop` - Workshops.
- `mentoring` - Mentoring sessions.
- `feedback` - Product feedback.
- `interaction` - Interaction with Googlers.
- `stories` - Success stories.
- `activities` - Retrieve submitted activities (`GET /activities`).
- `patch` - Partially update an existing activity (`PATCH /activities/{activityId}`).
- `list` - Discover available tags, countries, and types (`list all` outputs all enums as JSON).

---

## 🤖 MCP Server (`Advocu.McpServer` & `Advocu.Mcp.SSE`)

The repository also includes Model Context Protocol (MCP) servers (stdio and HTTP/SSE) exposing:
- **Resource:** `advocumcp://advocu/enums` (`application/json`) returning all Advocu API enumerations in a single JSON payload.
- **Tools:** `GetAdvocuEnums`, `GetActivities`, `PatchActivity`, and the 7 activity draft tools (`DraftContentCreationActivity`, `DraftPublicSpeakingActivity`, `DraftWorkshopActivity`, `DraftMentoringActivity`, `DraftProductFeedbackActivity`, `DraftInteractionWithGooglerActivity`, `DraftStoriesActivity`).

---

## Feedback & Support ✨

We'd love to hear from you! If you have ideas, find bugs, or just want to say hi, please open an issue in this repository.

## License 📜

This project is licensed under the [Apache-2.0 License](LICENSE).

--- 

Created by [Jochen Kirstätter](https://jochen.kirstaetter.name/).
