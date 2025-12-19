using Advocu.NuGet.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Net.Http;

namespace Advocu.NuGet.Commands;

internal class PublicSpeakingCommand : ActivityCommand<PublicSpeakingSettings>
{
    public PublicSpeakingCommand(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public override async Task<int> ExecuteAsync(CommandContext context, PublicSpeakingSettings settings, System.Threading.CancellationToken cancellationToken)
    {
        var client = CreateClient(settings);

        var request = new CreatePublicSpeakingActivityRequest
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
            var response = await client.PostPublicSpeakingActivityAsync(request)
                .RunWithStatusAsync("Posting Public Speaking Activity...");

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
