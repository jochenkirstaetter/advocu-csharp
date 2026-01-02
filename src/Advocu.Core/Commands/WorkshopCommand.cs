using Advocu.NuGet.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Net.Http;

namespace Advocu.NuGet.Commands;

internal sealed class WorkshopCommand : ActivityCommand<WorkshopSettings>
{
    public WorkshopCommand(IHttpClientFactory httpClientFactory, TokenManager tokenManager) : base(httpClientFactory, tokenManager)
    {
    }

    public override async Task<int> ExecuteAsync(CommandContext context, WorkshopSettings settings, System.Threading.CancellationToken cancellationToken)
    {
        var client = CreateClient(settings);

        var request = new CreateWorkshopActivityRequest
        {
            Title = settings.Title,
            Description = settings.Description,
            Tags = ParseTags(settings.Tags),
            Attendees = settings.Attendees,
            EventFormat = settings.EventFormat,
            Country = settings.Country,
            InPersonAttendees = settings.InPersonAttendees,
            ActivityDate = settings.ActivityDate ?? DateTime.UtcNow.ToString("yyyy-MM-dd"),
            ActivityUrl = settings.ActivityUrl,
            AdditionalInfo = settings.AdditionalInfo,
            Private = settings.Private
        };

        ValidateRequest(request);

        try
        {
            var response = await client.PostWorkshopActivityAsync(request, cancellationToken)
                .RunWithStatusAsync("Posting Workshop Activity...");

            AnsiConsole.MarkupLine($"[green]Success![/] Activity Draft Created. ID: [bold]{response.Id}[/]");
            return 0;
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            return 1;
        }
    }
}
