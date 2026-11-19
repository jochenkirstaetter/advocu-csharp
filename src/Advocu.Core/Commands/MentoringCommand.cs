using Advocu.Core.Settings;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Advocu.Core.Commands;

/// <summary>
/// Command to create a mentoring activity draft.
/// </summary>
internal sealed class MentoringCommand : ActivityCommand<MentoringSettings>
{
    public MentoringCommand(IHttpClientFactory httpClientFactory, TokenManager tokenManager) : base(httpClientFactory, tokenManager)
    {
    }

    /// <inheritdoc />
    public override async Task<int> ExecuteAsync(CommandContext context, MentoringSettings settings, System.Threading.CancellationToken cancellationToken)
    {
        var client = CreateClient(settings);

        var request = new CreateMentoringActivityRequest
        {
            Title = settings.Title,
            Description = settings.Description,
            Tags = ParseTags(settings.Tags),
            Attendees = settings.Attendees,
            EventFormat = settings.EventFormat,
            ActivityDate = settings.ActivityDate ?? DateTime.UtcNow.ToString("yyyy-MM-dd"),
            ActivityUrl = settings.ActivityUrl ?? "",
            AdditionalInfo = settings.AdditionalInfo,
            Private = settings.Private
        };

        ValidateRequest(request);

        try
        {
            var response = await client.PostMentoringActivityAsync(request, cancellationToken)
                .RunWithStatusAsync("Posting Mentoring Activity...");

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
