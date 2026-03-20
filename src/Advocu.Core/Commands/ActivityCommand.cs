using Advocu.Core.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using DA = System.ComponentModel.DataAnnotations;

namespace Advocu.Core.Commands;

/// <summary>
/// Base class for all activity-related commands.
/// </summary>
/// <typeparam name="TSettings">The type of settings used by the command.</typeparam>
internal abstract class ActivityCommand<TSettings> : AsyncCommand<TSettings> where TSettings : AdvocuSettings
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly TokenManager _tokenManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActivityCommand{TSettings}"/> class.
    /// </summary>
    /// <param name="httpClientFactory">The HTTP client factory.</param>
    /// <param name="tokenManager">The token manager.</param>
    protected ActivityCommand(IHttpClientFactory httpClientFactory, TokenManager tokenManager)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _tokenManager = tokenManager ?? throw new ArgumentNullException(nameof(tokenManager));
    }

    /// <summary>
    /// Creates an instance of the <see cref="AdvocuApiClient"/> based on the settings.
    /// </summary>
    /// <param name="settings">The settings containing configuration details.</param>
    /// <returns>A configured <see cref="AdvocuApiClient"/>.</returns>
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

    /// <summary>
    /// Parses a string array of tags into a list of <see cref="AdvocuTag"/>.
    /// </summary>
    /// <param name="tags">The string array of tags to parse.</param>
    /// <returns>A list of parsed tags.</returns>
    protected static List<AdvocuTag> ParseTags(string[] tags)
    {
        return [
            .. tags
                .SelectMany(t => t.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                .Select(Parsing.AdvocuEnumParser.Parse<AdvocuTag>)
                .Distinct()
                .OrderBy(t => t)
        ];
    }

    /// <summary>
    /// Validates the given request model using data annotations.
    /// </summary>
    /// <param name="request">The request model to validate.</param>
    protected static void ValidateRequest(object request)
    {
        var context = new DA.ValidationContext(request, serviceProvider: null, items: null);
        List<DA.ValidationResult> results = [];

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
