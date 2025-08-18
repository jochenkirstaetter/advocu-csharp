using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Your client namespace
namespace Advocu;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var client = services.GetRequiredService<Advocu.AdvocuApiClient>();

                // Example usage:
                var request = new CreateContentCreationActivityRequest
                {
                    ContentType = AdvocuActivityContentType.Articles,
                    Title = "How AI is Revolutionizing Software Development",
                    Description = "This article explores the transformative impact of artificial intelligence on modern software development practices, including AI-powered code generation, testing, and deployment. It delves into various tools and methodologies that are emerging in the AI-driven development landscape.",
                    Tags = new List<string> { "AI", "Build with AI", "Google Cloud", "AI - Generative AI" },
                    Readers = 500,
                    ActivityDate = DateTime.UtcNow.ToString("yyyy-MM-dd"), // Format date as required
                    ActivityUrl = "https://example.com/my-ai-article",
                    AdditionalInfo = "This is a draft for an upcoming blog post.",
                    Private = false
                };

                Console.WriteLine("Attempting to post content creation activity draft...");
                var response = await client.PostContentCreationActivityAsync(request);

                Console.WriteLine($"Activity Draft Posted Successfully!");
                Console.WriteLine($"Draft ID: {response.Id}");
                Console.WriteLine($"Status: {response.Status}");
                
                Console.WriteLine("\n--- Posting Public Speaking Activity Draft ---");
                var publicSpeakingRequest = new CreatePublicSpeakingActivityRequest
                {
                    Title = "My Talk on Cloud Native Development",
                    Description = "A session covering best practices and tools for building cloud-native applications on Google Cloud Platform.",
                    Tags = new List<string> { "Cloud", "Google_Cloud", "Cloud_App_Development", "Technology_Open_source" },
                    Attendees = 150,
                    EventFormat = AdvocuEventFormat.In_Person,
                    Country = AdvocuCountry.United_States_of_America,
                    InPersonAttendees = 100,
                    ActivityDate = "2024-10-26",
                    ActivityUrl = "https://gdg.community.dev/events/my-talk-event",
                    AdditionalInfo = "Presented at the DevFest 2024.",
                    Private = false
                };

                Console.WriteLine("Attempting to post public speaking activity draft...");
                var publicSpeakingResponse = await client.PostPublicSpeakingActivityAsync(publicSpeakingRequest);

                Console.WriteLine($"Public Speaking Activity Draft Posted Successfully!");
                //Console.WriteLine($"Draft ID: {publicSpeakingResponse.Id}");
                //Console.WriteLine($"Status: {publicSpeakingResponse.Status}");
            }
            catch (ValidationException validationEx)
            {
                Console.Error.WriteLine($"Client-side Validation Error: {validationEx.Message}");
            }
            catch (HttpRequestException httpEx)
            {
                Console.Error.WriteLine($"API Request Error: {httpEx.Message}");
                if (httpEx.StatusCode.HasValue)
                {
                    Console.Error.WriteLine($"Status Code: {(int)httpEx.StatusCode.Value} {httpEx.StatusCode.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        await host.RunAsync(); // Keep the application running if it's a web app/service
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddEnvironmentVariables();
                // In production, consider using User Secrets or Azure Key Vault for sensitive data
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Bind configuration to the settings model
                services.Configure<AdvocuApiSettings>(hostContext.Configuration.GetSection("AdvocuApiSettings"));

                // Register IHttpClientFactory
                services.AddHttpClient();

                // Register your Advocu
                services.AddScoped<Advocu.AdvocuApiClient>(serviceProvider =>
                {
                    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                    var httpClient = httpClientFactory.CreateClient("AdvocuApi"); // Use a named client for specific configurations

                    // Retrieve API settings
                    var apiSettings = hostContext.Configuration.GetSection("AdvocuApiSettings").Get<AdvocuApiSettings>();
                    if (apiSettings == null || string.IsNullOrEmpty(apiSettings.AccessToken))
                    {
                        throw new InvalidOperationException("Advocu API settings or AccessToken not configured.");
                    }
                    //httpClient.BaseAddress = new Uri(apiSettings.BaseUrl);

                    return new Advocu.AdvocuApiClient(httpClient, apiSettings.AccessToken);
                });

                // You can add other services here if needed
            });
}