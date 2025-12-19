using System.ComponentModel;
using Spectre.Console.Cli;

namespace Advocu.NuGet.Settings;

internal class AdvocuSettings : CommandSettings
{
    [CommandOption("-a|--api-token <TOKEN>")]
    [Description("The Advocu API Token. Defaults to ADVOCU_API_TOKEN environment variable.")]
    public string? ApiToken { get; set; }

    [CommandOption("-u|--api-url <URL>")]
    [Description("The Advocu API URL. Defaults to ADVOCU_URL environment variable.")]
    public string? ApiUrl { get; set; }

    [CommandOption("-p|--private")]
    [Description("Mark the activity as private.")]
    public bool Private { get; set; }

    public string GetAccessToken()
    {
        var token = ApiToken ?? Environment.GetEnvironmentVariable("ADVOCU_API_TOKEN");
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidOperationException("API Token is missing. Provide it via -a/--api-token or ADVOCU_API_TOKEN environment variable.");
        }
        return token;
    }

    public string GetApiUrl()
    {
        var url = ApiUrl ?? Environment.GetEnvironmentVariable("ADVOCU_URL");
        return !string.IsNullOrWhiteSpace(url) ? url : "https://api.advocu.com/personal-api/v1/gde/";
    }
}
