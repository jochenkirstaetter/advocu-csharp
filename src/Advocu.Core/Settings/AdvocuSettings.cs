using System.ComponentModel;
using Spectre.Console.Cli;

namespace Advocu.Core.Settings;

/// <summary>
/// Base class for all command settings, providing common API configuration options.
/// </summary>
internal class AdvocuSettings : CommandSettings
{
    /// <summary>
    /// Gets or sets the Advocu API token.
    /// </summary>
    [CommandOption("-a|--api-token <TOKEN>")]
    [Description("The Advocu API Token. Defaults to ADVOCU_API_TOKEN environment variable.")]
    public string? ApiToken { get; set; }

    /// <summary>
    /// Gets or sets the Advocu API URL.
    /// </summary>
    [CommandOption("-u|--api-url <URL>")]
    [Description("The Advocu API URL. Defaults to ADVOCU_URL environment variable.")]
    public string? ApiUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the activity should be marked as private.
    /// </summary>
    [CommandOption("-p|--private")]
    [Description("Mark the activity as private.")]
    public bool Private { get; set; }

    /// <summary>
    /// Retrieves the access token from the settings or environment variable.
    /// </summary>
    /// <returns>The access token.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the token is not found.</exception>
    public string GetAccessToken()
    {
        var token = ApiToken ?? Environment.GetEnvironmentVariable("ADVOCU_API_TOKEN");
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidOperationException("API Token is missing. Provide it via -a/--api-token or ADVOCU_API_TOKEN environment variable.");
        }
        return token;
    }

    /// <summary>
    /// Retrieves the API URL from the settings or environment variable.
    /// </summary>
    /// <returns>The API URL.</returns>
    public string GetApiUrl()
    {
        var url = ApiUrl ?? Environment.GetEnvironmentVariable("ADVOCU_URL");
        return !string.IsNullOrWhiteSpace(url) ? url : "https://api.advocu.com/personal-api/v1/gde/";
    }
}
