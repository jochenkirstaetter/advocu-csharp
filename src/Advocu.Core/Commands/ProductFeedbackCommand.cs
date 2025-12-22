using Advocu.NuGet.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Net.Http;

namespace Advocu.NuGet.Commands;

internal class ProductFeedbackCommand : ActivityCommand<ProductFeedbackSettings>
{
    public ProductFeedbackCommand(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public override async Task<int> ExecuteAsync(CommandContext context, ProductFeedbackSettings settings, System.Threading.CancellationToken cancellationToken)
    {
        var client = CreateClient(settings);

        var request = new CreateProductFeedbackActivityRequest
        {
            Title = settings.Title,
            Description = settings.Description,
            Tags = ParseTags(settings.Tags),
            ContentType = settings.ContentType,
            ProductDescription = settings.ProductDescription,
            TimeSpent = settings.TimeSpent,
            ActivityDate = settings.ActivityDate ?? DateTime.UtcNow.ToString("yyyy-MM-dd"),
            AdditionalInfo = settings.AdditionalInfo,
            Private = settings.Private
        };

        ValidateRequest(request);

        try
        {
            var response = await client.PostProductFeedbackActivityAsync(request, cancellationToken)
                .RunWithStatusAsync("Posting Product Feedback Activity...");

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
