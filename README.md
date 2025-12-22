# Advocu C#

An Advocu client for .NET

👋 Hello Google Developer Experts! 

Reporting your awesome community work shouldn't be a chore. Whether you want to integrate reporting directly into your own .NET applications or prefer a quick command-line tool to submit your activities, we've got you covered. This library provides a friendly C# client for the Advocu Personal API and a powerful CLI tool to streamline your GDE reporting activities.

## 🚀 Getting Started

Get your Advocu Personal API token from [Advocu](https://app.advocu.com/settings/integrations/personal-api) and store it in an environment variable `ADVOCU_API_TOKEN`. Alternatively, you can pass it as a command-line argument `--api-token` to the CLI tool.

### Ad hoc without installation (.NET 10)

Run the `dnx` command to use the Advocu CLI tool:

```bash
dnx advocu -y
```

Use double-dash `--` to pass arguments to the Advocu CLI tool:

```bash
dnx advocu -y -- content -t "Creating Advocu C# using Antigravity" -d "This blog article describes..."
```

### CLI Tool
Install the `advocu` global tool to use it from anywhere:

```bash
dotnet tool install --global Advocu
```

### Library
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
    content        Creates a new Content Creation Activity draft         
    public         Creates a new Public Speaking Activity draft          
    workshop       Creates a new Workshop Activity draft                 
    mentoring      Creates a new Mentoring Activity draft                
    feedback       Creates a new Product Feedback Activity draft         
    interaction    Creates a new Interaction with Googlers Activity draft
    stories        Creates a new Stories Activity draft                  
    list           Lists available options for enums 
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

Check out the `Advocu.Console` and `Advocu.WebApp` projects in this repository for more complete examples!

---

## 🛠️ The `advocu` CLI

Prefer the terminal? The `advocu` CLI allows you to draft activities without leaving your command line. Perfect for scripting or quick reports!

### Configuration
You can pass your API token and URL via flags (`--api-token`, `-u`) or, for convenience, set them as environment variables:
- `ADVOCU_API_TOKEN`
- `ADVOCU_URL` (default: `https://api.advocu.com/personal-api/v1/gde/`)

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

### Available Commands
- `content` - Articles, videos, etc.
- `public` - Public speaking.
- `workshop` - Workshops.
- `mentoring` - Mentoring sessions.
- `feedback` - Product feedback.
- `interaction` - Interaction with Googlers.
- `stories` - Success stories.
- `list` - Discover available tags, countries, and types.

---

## Feedback & Support ✨

We'd love to hear from you! If you have ideas, find bugs, or just want to say hi, please open an issue in this repository.

## License 📜

This project is licensed under the [Apache-2.0 License](LICENSE).

--- 

Created by [Jochen Kirstätter](https://jochen.kirstaetter.name/).
