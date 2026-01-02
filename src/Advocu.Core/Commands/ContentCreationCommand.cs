using Advocu.NuGet.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Net.Http;

namespace Advocu.NuGet.Commands;

internal sealed class ContentCreationCommand : ActivityCommand<ContentCreationSettings>
{
    public ContentCreationCommand(IHttpClientFactory httpClientFactory, TokenManager tokenManager) : base(httpClientFactory, tokenManager)
    {
    }

    public override async Task<int> ExecuteAsync(CommandContext context, ContentCreationSettings settings, System.Threading.CancellationToken cancellationToken)
    {
        var client = CreateClient(settings);

        var request = new CreateContentCreationActivityRequest
        {
            Title = settings.Title,
            Description = settings.Description,
            ContentType = settings.ContentType,
            Tags = ParseTags(settings.Tags),
            Readers = settings.Readers,
            ActivityDate = settings.ActivityDate ?? DateTime.UtcNow.ToString("yyyy-MM-dd"),
            ActivityUrl = settings.ActivityUrl,
            AdditionalInfo = settings.AdditionalInfo,
            Private = settings.Private
        };

        ValidateRequest(request);

        try
        {
            var response = await client.PostContentCreationActivityAsync(request, cancellationToken)
                .RunWithStatusAsync("Posting Content Creation Activity...");

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
