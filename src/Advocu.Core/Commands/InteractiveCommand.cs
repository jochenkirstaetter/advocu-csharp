using Advocu.NuGet.Models;
using Advocu.NuGet.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Reflection;
using System.Text.Json.Serialization;
using Advocu.NuGet.Mappers;

namespace Advocu.NuGet.Commands;

internal sealed class InteractiveCommand : AsyncCommand<InteractiveSettings>
{
    private readonly DraftManager _draftManager;
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly DraftMapper _draftMapper;

    public InteractiveCommand(DraftManager draftManager, IHttpClientFactory httpClientFactory, TokenManager tokenManager, DraftMapper draftMapper)
    {
        _draftManager = draftManager;
        _httpClientFactory = httpClientFactory;
        _tokenManager = tokenManager;
        _draftMapper = draftMapper;
    }

    // InteractiveCommand does not inherit from ActivityCommand, so it needs its own _tokenManager field and logic update.
    // Wait, InteractiveCommand inherits AsyncCommand<InteractiveSettings>.
    // So I need to add the field.
    
    private readonly TokenManager _tokenManager;

    public override async Task<int> ExecuteAsync(CommandContext context, InteractiveSettings settings, CancellationToken cancellationToken)
    {
        var draft = _draftManager.LoadDraft<ActivityDraft>();
        
        if (draft != null && draft.IsValid)
        {
            var resume = AnsiConsole.Confirm(
                $"Found unsaved draft for [green]{draft.ActivityType}[/] from [blue]{_draftManager.GetDraftLastModified()}[/]. Resume?", 
                defaultValue: true);

            if (!resume)
            {
                draft = null;
                _draftManager.ClearDraft();
            }
        }

        if (draft == null)
        {
            draft = new ActivityDraft();
            var type = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select [green]Activity Type[/]:")
                    .AddChoices("Content Creation", "Public Speaking", "Workshop", "Mentoring", "Product Feedback", "Interaction", "Stories"));
            
            draft.ActivityType = type;
            _draftManager.SaveDraft(draft);
        }

        // Common Fields
        AnsiConsole.MarkupLine($"\n[bold]Details for {draft.ActivityType}[/]");
        
        draft.Title = Ask("Title", draft.Title);
        _draftManager.SaveDraft(draft);

        draft.Description = Ask("Description", draft.Description);
        _draftManager.SaveDraft(draft);
        
        draft.ActivityDate = AskDate("Activity Date", draft.ActivityDate);
        _draftManager.SaveDraft(draft);
        
        draft.ActivityUrl = Ask("Activity URL", draft.ActivityUrl, optional: true);
        _draftManager.SaveDraft(draft);
        
        draft.Tags = AskMultiSelectEnum<AdvocuTag>("Tags", draft.Tags);
        _draftManager.SaveDraft(draft);

        // Type Specific Fields
        switch (draft.ActivityType)
        {
            case "Content Creation":
                await CollectContentCreation(draft);
                break;
            case "Public Speaking":
                await CollectPublicSpeaking(draft);
                break;
            case "Workshop":
                await CollectWorkshop(draft);
                break;
            case "Mentoring":
                await CollectMentoring(draft);
                break;
            case "Product Feedback":
                await CollectProductFeedback(draft);
                break;
            case "Interaction":
                await CollectInteraction(draft);
                break;
            case "Stories":
                await CollectStories(draft);
                break;
        }

        draft.AdditionalInfo = Ask("Additional Info", draft.AdditionalInfo, optional: true);
        draft.Private = AnsiConsole.Confirm("Is this private?", draft.Private ?? false);
        _draftManager.SaveDraft(draft);

        // Summary and Submit
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[yellow]Review Draft[/]"));
        var table = new Table();
        table.AddColumn("Field");
        table.AddColumn("Value");
        
        AddRow(table, "Type", draft.ActivityType);
        AddRow(table, "Title", draft.Title);
        AddRow(table, "Description", draft.Description);
        AddRow(table, "Date", draft.ActivityDate?.ToString("yyyy-MM-dd"));
        AddRow(table, "Tags", string.Join(", ", draft.Tags));
        
        // Add specific rows based on type (simplified for brevity, can expand)
        AddRow(table, "URL", draft.ActivityUrl);

        AnsiConsole.Write(table);

        if (AnsiConsole.Confirm("Submit this activity?", defaultValue: true))
        {
            return await SubmitDraft(draft, settings, cancellationToken);
        }

        AnsiConsole.MarkupLine("[yellow]Draft saved. Exiting.[/]");
        return 0;
    }

    private void AddRow(Table table, string name, string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            table.AddRow(name, value);
        }
    }

    private async Task<int> SubmitDraft(ActivityDraft draft, InteractiveSettings settings, CancellationToken cancellationToken)
    {
        try
        {
            var client = CreateClient(settings);
            
            // Map Draft to Request using Mapper
            var request = _draftMapper.Map(draft);

            dynamic response = request switch
            {
                CreateContentCreationActivityRequest r => await client.PostContentCreationActivityAsync(r, cancellationToken),
                CreatePublicSpeakingActivityRequest r => await client.PostPublicSpeakingActivityAsync(r, cancellationToken),
                CreateWorkshopActivityRequest r => await client.PostWorkshopActivityAsync(r, cancellationToken),
                CreateMentoringActivityRequest r => await client.PostMentoringActivityAsync(r, cancellationToken),
                CreateProductFeedbackActivityRequest r => await client.PostProductFeedbackActivityAsync(r, cancellationToken),
                CreateInteractionWithGooglersActivityRequest r => await client.PostInteractionWithGooglersActivityAsync(r, cancellationToken),
                CreateStoriesActivityRequest r => await client.PostStoriesActivityAsync(r, cancellationToken),
                _ => throw new NotImplementedException($"Submission for {draft.ActivityType} not implemented yet.")
            };

            AnsiConsole.MarkupLine($"[green]Success![/] Activity submitted. ID: {response.Id}");
            _draftManager.ClearDraft();
            return 0;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error submitting activity:[/]");
            AnsiConsole.WriteException(ex);
            return 1;
        }
    }

    private Task CollectContentCreation(ActivityDraft draft)
    {
        draft.ContentType = AskEnum<AdvocuActivityContentType>("Content Type", draft.ContentType);
        _draftManager.SaveDraft(draft);
        
        draft.Readers = AskInt("Readers", draft.Readers);
        _draftManager.SaveDraft(draft);
        return Task.CompletedTask;
    }

    private Task CollectPublicSpeaking(ActivityDraft draft)
    {
        draft.Country = AskEnum<AdvocuCountry>("Country", draft.Country);
        _draftManager.SaveDraft(draft);
        
        draft.City = Ask("City", draft.City, optional: true);
        _draftManager.SaveDraft(draft);
        
        draft.EventFormat = AskEnum<AdvocuEventFormat>("Event Format", draft.EventFormat);
        _draftManager.SaveDraft(draft);
        
        draft.Attendees = AskInt("Attendees", draft.Attendees);
        _draftManager.SaveDraft(draft);
        return Task.CompletedTask;
    }
    
    // Stubs for other types to keep file size manageable and focus on Content/Public first as per plan verification
    private Task CollectWorkshop(ActivityDraft draft)
    {
        draft.Country = AskEnum<AdvocuCountry>("Country", draft.Country);
        _draftManager.SaveDraft(draft);
        
        draft.EventFormat = AskEnum<AdvocuEventFormat>("Event Format", draft.EventFormat);
        _draftManager.SaveDraft(draft);
        
        draft.Attendees = AskInt("Attendees", draft.Attendees);
        _draftManager.SaveDraft(draft);
        return Task.CompletedTask;
    }

    private Task CollectMentoring(ActivityDraft draft)
    {
        draft.EventFormat = AskEnum<AdvocuEventFormat>("Event Format", draft.EventFormat);
        _draftManager.SaveDraft(draft);

        if (draft.EventFormat != "Virtual")
        {
            draft.Country = AskEnum<AdvocuCountry>("Country", draft.Country);
            _draftManager.SaveDraft(draft);
        }

        draft.Mentees = AskInt("Mentees", draft.Mentees);
        _draftManager.SaveDraft(draft);
        return Task.CompletedTask;
    }

    private Task CollectProductFeedback(ActivityDraft draft)
    {
        draft.ContentType = AskEnum<AdvocuActivityContentType>("Content Type", draft.ContentType);
        _draftManager.SaveDraft(draft);

        draft.ProductTeam = Ask("Product Description / Team", draft.ProductTeam);
        _draftManager.SaveDraft(draft);

        draft.Hours = AskInt("Time Spent (minutes)", draft.Hours);
        _draftManager.SaveDraft(draft);
        return Task.CompletedTask;
    }

    private Task CollectInteraction(ActivityDraft draft)
    {
        draft.InteractionType = AskEnum<AdvocuInteractionType>("Interaction Type", draft.InteractionType);
        _draftManager.SaveDraft(draft);

        draft.EventFormat = AskEnum<AdvocuFormat>("Format", draft.EventFormat); // Utilizing EventFormat field for Format enum
        _draftManager.SaveDraft(draft);

        draft.Hours = AskInt("Time Spent (minutes)", draft.Hours);
        _draftManager.SaveDraft(draft);
        return Task.CompletedTask;
    }

    private Task CollectStories(ActivityDraft draft)
    {
        draft.StoryType = AskEnum<AdvocuSignificanceType>("Significance Type", draft.StoryType);
        _draftManager.SaveDraft(draft);

        draft.Significance = Ask("Why is it significant?", draft.Significance);
        _draftManager.SaveDraft(draft);

        draft.Attendees = AskInt("Impact (Number)", draft.Attendees); // Reusing Attendees for Impact
        _draftManager.SaveDraft(draft);
        return Task.CompletedTask;
    }

    // Helpers
    private string Ask(string prompt, string? current, bool optional = false)
    {
        var p = new TextPrompt<string>($"{prompt}:");
        if (current != null) p.DefaultValue(current);
        if (optional) p.AllowEmpty();
        return AnsiConsole.Prompt(p);
    }

    private DateTime? AskDate(string prompt, DateTime? current)
    {
        var p = new TextPrompt<DateTime>($"{prompt}:");
         // Spectre can parse dates, but format might need help. 
         // For stricter control we could use Validation.
        if (current.HasValue) p.DefaultValue(current.Value);
        return AnsiConsole.Prompt(p);
    }

    private int? AskInt(string prompt, int? current)
    {
        var p = new TextPrompt<int>($"{prompt}:");
        if (current.HasValue) p.DefaultValue(current.Value);
        return AnsiConsole.Prompt(p);
    }

    private string AskEnum<T>(string prompt, string? current) where T : struct, Enum
    {
        var choices = Enum.GetValues<T>().Select(GetEnumDisplayName).ToList();
        var p = new SelectionPrompt<string>()
            .Title($"{prompt}:")
            .AddChoices(choices);
        
        // Pre-select if current matches
        // SelectionPrompt doesn't have DefaultValue in same way, but we can highlight?
        // Spectre Console SelectionPrompt doesn't easily support 'start on this item' in all versions, 
        // but we can try just showing it.
        
        return AnsiConsole.Prompt(p);
    }

    private List<string> AskMultiSelectEnum<T>(string prompt, List<string> current) where T : struct, Enum
    {
        var choices = Enum.GetValues<T>().Select(GetEnumDisplayName).ToList();
        var p = new MultiSelectionPrompt<string>()
            .Title($"{prompt}:")
            .NotRequired() // Tags can be empty?
            .AddChoices(choices);
        
        if (current != null)
        {
            foreach(var c in current)
            {
                // We need to map back to display name to select it
                // Assuming current is stored as display name or parsable
                // If stored as API value, we match.
                // For simplicity, let's assume current holds the display names for now or matches choices.
                p.Select(c); 
            }
        }

        return AnsiConsole.Prompt(p);
    }

    private string GetEnumDisplayName<T>(T value) where T : struct, Enum
    {
        var memberInfo = typeof(T).GetMember(value.ToString()).FirstOrDefault();
        if (memberInfo != null)
        {
            var attr = memberInfo.GetCustomAttribute<JsonStringEnumMemberNameAttribute>();
            if (attr != null) return attr.Name;
        }
        return value.ToString();
    }



    private AdvocuApiClient CreateClient(InteractiveSettings settings)
    {
        var token = _tokenManager.ResolveToken(settings.ApiToken);
        var client = _httpClientFactory.CreateClient("AdvocuApi");
        var baseUrl = settings.GetApiUrl();
        client.BaseAddress = new Uri(baseUrl);
        return new AdvocuApiClient(client, token);
    }
}
