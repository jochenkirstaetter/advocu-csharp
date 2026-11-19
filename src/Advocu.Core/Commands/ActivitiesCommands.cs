using Advocu.Core.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Globalization;
using System.Text.Json;

namespace Advocu.Core.Commands;

/// <summary>
/// Command to retrieve activities via <c>GET /activities</c>.
/// </summary>
internal sealed class GetActivitiesCommand(IHttpClientFactory httpClientFactory, TokenManager tokenManager)
    : ActivityCommand<GetActivitiesSettings>(httpClientFactory, tokenManager)
{
    private static readonly JsonSerializerOptions IndentedJson = new() { WriteIndented = true };

    public override async Task<int> ExecuteAsync(CommandContext context, GetActivitiesSettings settings, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetActivitiesQuery
            {
                From = ParseTimestamp(settings.From, "--from"),
                To = ParseTimestamp(settings.To, "--to"),
                UpdatedAfter = ParseTimestamp(settings.UpdatedAfter, "--updated-after"),
                Page = settings.Page,
                Size = settings.Size
            };

            var client = CreateClient(settings);
            var response = await client.GetActivitiesAsync(query, cancellationToken);

            if (settings.Json)
            {
                Console.WriteLine(JsonSerializer.Serialize(response, IndentedJson));
                return 0;
            }

            if (response.Content.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No activities found for the specified query.[/]");
                return 0;
            }

            var table = new Table();
            table.AddColumn("Activity ID");
            table.AddColumn("Type");
            table.AddColumn("Date");
            table.AddColumn("Title");
            table.AddColumn("Updated");

            foreach (var item in response.Content)
            {
                table.AddRow(
                    Markup.Escape(item.ActivityId),
                    Markup.Escape(item.Type),
                    Markup.Escape(item.Data.ActivityDate ?? string.Empty),
                    Markup.Escape(item.Data.Title ?? string.Empty),
                    Markup.Escape(item.UpdatedDate));
            }

            AnsiConsole.Write(table);
            return 0;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error:[/] {Markup.Escape(ex.Message)}");
            return 1;
        }
    }

    private static DateTimeOffset? ParseTimestamp(string? value, string optionName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (!DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var parsed))
        {
            throw new ArgumentException($"Invalid timestamp for {optionName}: '{value}'. Use ISO-8601 format (e.g. 2026-01-01T00:00:00Z).");
        }

        return parsed;
    }
}

/// <summary>
/// Command to partially update an activity via <c>PATCH /activities/{activityId}</c>.
/// </summary>
internal sealed class PatchActivityCommand(IHttpClientFactory httpClientFactory, TokenManager tokenManager)
    : ActivityCommand<PatchActivitySettings>(httpClientFactory, tokenManager)
{
    private static readonly JsonSerializerOptions IndentedJson = new() { WriteIndented = true };

    public override async Task<int> ExecuteAsync(CommandContext context, PatchActivitySettings settings, CancellationToken cancellationToken)
    {
        try
        {
            ActivityMetrics? metrics = null;
            if (settings.Readers.HasValue || settings.Attendees.HasValue || settings.TimeSpent.HasValue || settings.Impact.HasValue)
            {
                metrics = new ActivityMetrics
                {
                    Readers = settings.Readers,
                    Attendees = settings.Attendees,
                    TimesSpent = settings.TimeSpent,
                    Impact = settings.Impact
                };
            }

            List<string>? tags = null;
            if (settings.Tags is { Length: > 0 })
            {
                tags = ParseTags(settings.Tags).Select(AdvocuEnumsSnapshot.GetApiValue).ToList();
            }

            var request = new PatchActivityRequest
            {
                Data = new PatchActivityData
                {
                    Title = settings.Title,
                    Description = settings.Description,
                    ContentType = settings.ContentType,
                    ProductDescription = settings.ProductDescription,
                    Format = settings.Format,
                    InteractionType = settings.InteractionType,
                    EventFormat = settings.EventFormat,
                    Country = settings.Country,
                    InPersonAttendees = settings.InPersonAttendees,
                    WhyIsSignificant = settings.WhyIsSignificant,
                    SignificanceType = settings.SignificanceType,
                    Tags = tags,
                    Metrics = metrics,
                    ActivityDate = settings.ActivityDate,
                    ActivityUrl = settings.ActivityUrl,
                    AdditionalInfo = settings.AdditionalInfo,
                    AdditionalLinks = settings.AdditionalLinks,
                    Private = settings.SetPrivate
                }
            };

            var client = CreateClient(settings);
            var updated = await client.PatchActivityAsync(settings.ActivityId, request, cancellationToken);

            if (settings.Json)
            {
                Console.WriteLine(JsonSerializer.Serialize(updated, IndentedJson));
                return 0;
            }

            AnsiConsole.MarkupLine($"[green]Activity updated successfully![/] ID: [bold]{Markup.Escape(updated.ActivityId)}[/] ({Markup.Escape(updated.Type)})");
            return 0;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error:[/] {Markup.Escape(ex.Message)}");
            return 1;
        }
    }
}
