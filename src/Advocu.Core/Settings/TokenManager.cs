using System.Text.Json;
using System.Text.Json.Serialization;
using Spectre.Console;

namespace Advocu.NuGet.Settings;

/// <summary>
/// Manages the retrieval and storage of the Advocu API token.
/// </summary>
internal sealed class TokenManager
{
    private readonly string _tokenFilePath;

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenManager"/> class.
    /// </summary>
    public TokenManager()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var advocuDir = Path.Combine(appData, "advocu");
        if (!Directory.Exists(advocuDir))
        {
            Directory.CreateDirectory(advocuDir);
        }
        _tokenFilePath = Path.Combine(advocuDir, "token.json");
    }

    /// <summary>
    /// Resolves the API token using the priority order: Flag > Env Var > .env files > Stored File > Interactive Prompt.
    /// </summary>
    /// <param name="flagToken">The token provided via command-line argument.</param>
    /// <returns>The resolved API token.</returns>
    public string ResolveToken(string? flagToken)
    {
        // 1. Flag
        if (!string.IsNullOrWhiteSpace(flagToken))
        {
            return flagToken;
        }

        // 2. Env Var
        var envToken = Environment.GetEnvironmentVariable("ADVOCU_API_TOKEN");
        if (!string.IsNullOrWhiteSpace(envToken))
        {
            return envToken;
        }

        // 3. .env File (local)
        var dotEnvToken = GetTokenFromDotEnv();
        if (!string.IsNullOrWhiteSpace(dotEnvToken))
        {
            return dotEnvToken;
        }

        // 4. Stored File
        var storedToken = GetStoredToken();
        if (!string.IsNullOrWhiteSpace(storedToken))
        {
            return storedToken;
        }

        // 5. Prompt
        AnsiConsole.MarkupLine("[yellow]No API Token found.[/]");
        var newToken = AnsiConsole.Prompt(
            new TextPrompt<string>("Please enter your [green]Advocu API Token[/]:")
                .Secret());

        if (!string.IsNullOrWhiteSpace(newToken))
        {
            SaveToken(newToken);
            AnsiConsole.MarkupLine("[gray]Token saved to profile.[/]");
        }

        return newToken;
    }

    private string? GetTokenFromDotEnv()
    {
        try 
        {
            var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
            if (!File.Exists(envPath)) return null;

            foreach (var line in File.ReadAllLines(envPath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;

                var parts = line.Split('=', 2);
                if (parts.Length != 2) continue;

                var key = parts[0].Trim();
                var value = parts[1].Trim();

                if (key.Equals("ADVOCU_API_TOKEN", StringComparison.OrdinalIgnoreCase))
                {
                    return value.Trim('"', '\'');
                }
            }
        }
        catch 
        { 
            // Ignore errors reading .env
        }
        return null;
    }

    private class TokenData
    {
        [JsonPropertyName("AdvocuApiToken")]
        public string? AdvocuApiToken { get; set; }
    }
    
    /// <summary>
    /// Retrieves the stored API token from the persistent file.
    /// </summary>
    /// <returns>The stored token, or null if not found or invalid.</returns>
    public string? GetStoredToken()
    {
        if (!File.Exists(_tokenFilePath))
        {
            return null;
        }

        try
        {
            var json = File.ReadAllText(_tokenFilePath);
            var data = JsonSerializer.Deserialize<TokenData>(json);
            return data?.AdvocuApiToken;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Saves the API token to persistent storage.
    /// </summary>
    /// <param name="token">The API token to save.</param>
    public void SaveToken(string token)
    {
        var data = new TokenData { AdvocuApiToken = token };
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_tokenFilePath, json);
    }
}
