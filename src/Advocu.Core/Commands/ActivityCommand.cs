using Advocu.NuGet.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using DA = System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace Advocu.NuGet.Commands;

internal abstract class ActivityCommand<TSettings> : AsyncCommand<TSettings> where TSettings : AdvocuSettings
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly TokenManager _tokenManager;

    protected ActivityCommand(IHttpClientFactory httpClientFactory, TokenManager tokenManager)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _tokenManager = tokenManager ?? throw new ArgumentNullException(nameof(tokenManager));
    }

    protected AdvocuApiClient CreateClient(TSettings settings)
    {
        // Use TokenManager to resolve token (Flag > Env > File > Prompt)
        var token = _tokenManager.ResolveToken(settings.ApiToken);
        
        var client = _httpClientFactory.CreateClient("AdvocuApi");
        
        // Ensure base address is set (preferring settings override)
        var baseUrl = settings.GetApiUrl();
        client.BaseAddress = new Uri(baseUrl);

        return new AdvocuApiClient(client, token);
    }

    protected List<AdvocuTag> ParseTags(string[] tags)
    {
        return tags
            .SelectMany(t => t.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            .Select(Parsing.AdvocuEnumParser.Parse<AdvocuTag>)
            .ToList();
    }

    protected void ValidateRequest(object request)
    {
        var context = new DA.ValidationContext(request, serviceProvider: null, items: null);
        var results = new List<DA.ValidationResult>();

        if (!DA.Validator.TryValidateObject(request, context, results, validateAllProperties: true))
        {
            AnsiConsole.MarkupLine("[red]Validation failed:[/]");
            foreach (var validationResult in results)
            {
                AnsiConsole.MarkupLine($"  - [red]{validationResult.ErrorMessage}[/]");
            }
            throw new DA.ValidationException("Request validation failed.");
        }
    }
}
