using Advocu.NuGet.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Net.Http;

namespace Advocu.NuGet.Commands;

internal sealed class StoriesCommand : ActivityCommand<StoriesSettings>
{
    public StoriesCommand(IHttpClientFactory httpClientFactory, TokenManager tokenManager) : base(httpClientFactory, tokenManager)
    {
    }

    public override async Task<int> ExecuteAsync(CommandContext context, StoriesSettings settings, System.Threading.CancellationToken cancellationToken)
    {
        var client = CreateClient(settings);

        var request = new CreateStoriesActivityRequest
        {
            Title = settings.Title,
            Description = settings.Description,
            Tags = ParseTags(settings.Tags),
            WhyIsSignificant = settings.WhyIsSignificant,
            SignificanceType = settings.SignificanceType,
            Impact = settings.Impact,
            AdditionalInfo = settings.AdditionalInfo,
            Private = settings.Private
        };

        ValidateRequest(request);

        try
        {
            var response = await client.PostStoriesActivityAsync(request, cancellationToken)
                .RunWithStatusAsync("Posting Stories Activity...");

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
