using System.Reflection;
using Advocu.NuGet.Commands;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;
using Advocu.NuGet.Mappers;

namespace Advocu.NuGet;

internal static class Program
{
    private const string ApiBaseUrl = "https://api.advocu.com/personal-api/v1/gde/";

    public static async Task<int> Main(string[] args)
    {
        if (args.Length == 0)
        {
            args = ["--help"];
        }

        if (args.Contains("-h") || args.Contains("--help"))
        {
            var versionString = Assembly.GetEntryAssembly()?
                                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                                .InformationalVersion
                                .ToString();
            
            AnsiConsole.MarkupLine($"[bold deepskyblue1]Advocu C#[/] - v{versionString}");
            AnsiConsole.MarkupLine("[italic]An Advocu client for .NET[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("Reporting your awesome community work shouldn't be a chore. Whether you want to integrate reporting directly into your own .NET applications or prefer a quick command-line tool to submit your activities, we've got you covered. This library provides a friendly C# client for the Advocu Personal API and a powerful CLI tool to streamline your GDE reporting activities.");
            AnsiConsole.WriteLine();
        }

        var services = new ServiceCollection();
        ConfigureServices(services);

        var registrar = new TypeRegistrar(services);
        var app = new CommandApp<RootCommand>(registrar);

        app.Configure(config =>
        {
            config.SetApplicationName("advocu");

            config.AddCommand<ContentCreationCommand>("content")
                .WithDescription("Creates a new Content Creation Activity draft.");
            
            config.AddCommand<PublicSpeakingCommand>("public")
                .WithDescription("Creates a new Public Speaking Activity draft.");

            config.AddCommand<WorkshopCommand>("workshop")
                .WithDescription("Creates a new Workshop Activity draft.");

            config.AddCommand<MentoringCommand>("mentoring")
                .WithDescription("Creates a new Mentoring Activity draft.");
            
            config.AddCommand<ProductFeedbackCommand>("feedback")
                .WithDescription("Creates a new Product Feedback Activity draft.");

            config.AddCommand<InteractionCommand>("interaction")
                .WithDescription("Creates a new Interaction with Googlers Activity draft.");

            config.AddCommand<StoriesCommand>("stories")

                .WithDescription("Creates a new Stories Activity draft.");

            config.AddCommand<InteractiveCommand>("interactive")
                .WithDescription("Starts an interactive session to create an activity draft.");

            config.AddBranch("list", list =>
            {
                list.SetDescription("Lists available options for enumerations.");
                
                list.AddCommand<ListTagsCommand>("tags")
                    .WithDescription("Lists available Tags.");
                
                list.AddCommand<ListContentTypesCommand>("content-types")
                    .WithDescription("Lists available Content Types.");
                
                list.AddCommand<ListCountriesCommand>("countries")
                    .WithDescription("Lists available Countries.");
                
                list.AddCommand<ListEventFormatsCommand>("formats")
                    .WithDescription("Lists available Event Formats.");
                
                list.AddCommand<ListInteractionTypesCommand>("interaction-types")
                    .WithDescription("Lists available Interaction Types.");
                
                list.AddCommand<ListSignificanceTypesCommand>("significance-types")
                    .WithDescription("Lists available Significance Types.");
            });
        });

        return await app.RunAsync(args);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddSingleton<Advocu.NuGet.Settings.DraftManager>();
        services.AddSingleton<Advocu.NuGet.Settings.TokenManager>();
        services.AddSingleton<DraftMapper>();
        services.AddScoped<AdvocuApiClient>(provider =>
        {
            var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("AdvocuApi");
            httpClient.BaseAddress = new Uri(ApiBaseUrl);

            // We will inject the token *inside* the command execution or via a factory that
            // accesses the settings. However, Spectre.Cli dependency injection happens *before*
            // parsing settings completely for the service scope. 
            // 
            // A common pattern with Spectre.Cli + DI is to pass the token via command settings 
            // and initialize the client *inside* the command's ExecuteAsync.
            //
            // But to satisfy DI here w/o the token yet... construction of AdvocuApiClient fails if token is null.
            // 
            // Refactoring: The AdvocuApiClient requires token in constructor. 
            // We can register a factory that reads ENV var as fallback, but the CLI arg -a overrides it.
            // 
            // Since we need to support -a <token>, which is parsed by Spectre, we should probably
            // instantiate the client INSIDE the command handler or use a simpler DI approach where 
            // we register a "TokenHolder" that is populated after parsing?
            //
            // Better approach for this specific CLI:
            // Register IHttpClientFactory.
            // In the Command.ExecuteAsync, read the settings (which contain the token),
            // and manually create the AdvocuApiClient from the factory.
            
            return null!; // We won't resolve this directly in this simple design, see Command implementation.
        });
    }
}
