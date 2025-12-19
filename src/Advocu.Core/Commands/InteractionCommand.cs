using Advocu.NuGet.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Net.Http;

namespace Advocu.NuGet.Commands;

internal class InteractionCommand : ActivityCommand<InteractionSettings>
{
    public InteractionCommand(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public override async Task<int> ExecuteAsync(CommandContext context, InteractionSettings settings, System.Threading.CancellationToken cancellationToken)
    {
        var client = CreateClient(settings);

        var request = new CreateInteractionWithGooglersActivityRequest
        {
            Title = settings.Title,
            Description = settings.Description,
            Tags = ParseTags(settings.Tags),
            TimeSpent = settings.TimeSpent,
            Format = settings.Format,
            InteractionType = settings.InteractionType,
            ActivityDate = settings.ActivityDate ?? DateTime.UtcNow.ToString("yyyy-MM-dd"),
            AdditionalLinks = settings.AdditionalLinks,
            AdditionalInfo = settings.AdditionalInfo,
            Private = settings.Private
        };

        ValidateRequest(request);

        try
        {
            var response = await client.PostInteractionWithGooglersActivityAsync(request)
                .RunWithStatusAsync("Posting Interaction with Googlers Activity...");

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
