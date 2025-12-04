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
                ActivityResponse response = new ActivityResponse { Id = "0" };

                Console.WriteLine("\n--- Posting Content Creation Activity Draft ---");
                var request = new CreateContentCreationActivityRequest
                {
                    ContentType = AdvocuActivityContentType.Articles,
                    Title = "How AI is Revolutionizing Software Development",
                    Description = "This article explores the transformative impact of artificial intelligence on modern software development practices, including AI-powered code generation, testing, and deployment. It delves into various tools and methodologies that are emerging in the AI-driven development landscape.",
                    Tags =
                    [
                        AdvocuTag.BuildWithAi, AdvocuTag.GoogleCloud, AdvocuTag.AiGenerativeAi, AdvocuTag.Ai
                    ],
                    Readers = 500,
                    ActivityDate = DateTime.UtcNow.ToString("yyyy-MM-dd"), // Format date as required
                    ActivityUrl = "https://example.com/my-ai-article",
                    AdditionalInfo = "This is a draft for an upcoming blog post.",
                    Private = false
                };
                // response = await client.PostContentCreationActivityAsync(request);
                
                Console.WriteLine("\n--- Posting Public Speaking Activity Draft ---");
                var publicSpeakingRequest = new CreatePublicSpeakingActivityRequest
                {
                    Title = "Gemini CLI - your toolset to do magic!",
                    Description = "Delivered a remote talk on Gemini CLI during DevFest season. Explaining the features of Gemini CLI and demoing some use cases - both local and in GitHub Actions. Showing the use of tools, commands, MCP servers, and integration with Jules agent.",
                    Tags =
                    [
                        AdvocuTag.BuildWithAi, AdvocuTag.DevFest, AdvocuTag.AiGemini, AdvocuTag.AiGenerativeAi,
                        AdvocuTag.Ai
                    ],
                    Attendees = 50,
                    EventFormat = AdvocuEventFormat.Hybrid,
                    Country = AdvocuCountry.Uzbekistan,
                    ActivityDate = "2025-11-29",
                    ActivityUrl = "https://gdg.community.dev/events/details/google-gdg-qarshi-presents-devfest-kashkadarya-2025-gdg-karshi/cohost-gdg-qarshi",
                    AdditionalInfo = "",
                    Private = false
                };
                // response = await client.PostPublicSpeakingActivityAsync(publicSpeakingRequest);

                Console.WriteLine("\n--- Posting Workshop Activity Draft ---");
                var workshopRequest = new CreateWorkshopActivityRequest()
                {
                    Title = "Gemini CLI - your toolset to do magic!",
                    Description = "Delivered a workshop on Gemini CLI during DevFest season. Explaining the features of Gemini CLI and demoing some use cases - both local and in GitHub Actions. Showing the use of tools, commands, MCP servers, and integration with Jules agent.",
                    Tags =
                    [
                        AdvocuTag.BuildWithAi, AdvocuTag.DevFest, AdvocuTag.AiGemini, AdvocuTag.AiGenerativeAi,
                        AdvocuTag.Ai
                    ],
                    Attendees = 150,
                    EventFormat = AdvocuEventFormat.Virtual,
                    Country = AdvocuCountry.Uzbekistan,
                    ActivityDate = "2025-11-29",
                    ActivityUrl = "https://gdg.community.dev/events/details/google-gdg-qarshi-presents-devfest-kashkadarya-2025-gdg-karshi/cohost-gdg-qarshi",
                    AdditionalInfo = "",
                    Private = false
                };
                // response = await client.PostWorkshopActivityAsync(workshopRequest);
                
                Console.WriteLine("\n--- Posting Mentoring Activity Draft ---");
                var mentoringRequest = new CreateMentoringActivityRequest()
                {
                    Title = "",
                    Description = "",
                    Tags = [],
                    Attendees = 1,
                    EventFormat = AdvocuEventFormat.Virtual,
                    ActivityDate = "2025-12-31",
                    ActivityUrl = "",
                    AdditionalInfo = "",
                    Private = false
                };
                // response = await client.PostMentoringActivityAsync(mentoringRequest);
                    
                Console.WriteLine("\n--- Posting Product Feedback Given Activity Draft ---");
                var productFeedbackRequest = new CreateProductFeedbackActivityRequest()
                {
                    Title = "",
                    Description = "",
                    ContentType = AdvocuActivityContentType.EarlyAccessProgram,
                    ProductDescription = "",
                    Tags = [],
                    TimeSpent = 30,
                    ActivityDate = "2025-12-31",
                    AdditionalInfo = "",
                    Private = false
                };
                // response = await client.PostProductFeedbackActivityAsync(productFeedbackRequest);
                
                Console.WriteLine("\n--- Posting Interaction with Googlers Activity Draft ---");
                var interactionRequest = new CreateInteractionWithGooglersActivityRequest()
                {
                    // Title = "Gemini 3 Feedback Roundtable - EMEA/AMER",
                    // Tags = new List<string> { "AI", "AI - AI Studio", "AI - Gemini", "AI - Generative AI", "AI - Vertex AI" },
                    Title = "Global Cloud GDE Hangout (Monthly)",
                    Tags = [AdvocuTag.CloudData, AdvocuTag.GoogleCloud],
                    // Title = "Cloud GDE Infra Hangout",
                    // Tags = new List<string> { "Cloud - Compute, Networking, Storage", "Google Cloud" },
                    // Title = "SIG Keras Community Meeting",
                    // Tags = new List<string> { "AI - Keras" },
                    // Title = "Google Developer Expert Feedback on Cloud Location Finder",
                    // Tags = new List<string> { "Google Maps Platform", "Google Cloud" },
                    Description = "Connect testers with Googlers for Q&A and feedback.",
                    Format = AdvocuFormat.OnlineFeedbackSessions,
                    InteractionType = AdvocuInteractionType.OtherInteractionsWithGoogleProductTeams,
                    TimeSpent = 45,
                    ActivityDate = "2025-12-04",
                    AdditionalInfo = "",
                    AdditionalLinks = "",
                    Private = false
                };
                // response = await client.PostInteractionWithGooglersActivityAsync(interactionRequest);
                
                Console.WriteLine("\n--- Posting Stories Activity Draft ---");
                var storiesRequest = new CreateStoriesActivityRequest()
                {
                    Title = "",
                    Description = "",
                    WhyIsSignificant = "",
                    SignificanceType = AdvocuSignificanceType.TechnologyOpenSource,
                    Tags = [],
                    Impact = 100,
                    AdditionalInfo = "",
                    Private = false
                };
                // response = await client.PostStoriesActivityAsync(storiesRequest);

                Console.WriteLine($"Activity Draft Posted Successfully!");
                Console.WriteLine($"Draft ID: {response.Id}");
                Console.WriteLine($"Status: {response.Status}");
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
                config.AddJsonFile("appsettings.user.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
                // In production, consider using User Secrets or Azure Key Vault for sensitive data
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Bind configuration to the settings model
                services.Configure<AdvocuApiSettings>(hostContext.Configuration.GetSection("AdvocuApiSettings"));

                // Register IHttpClientFactory
                services.AddHttpClient();

                // Register your Advocu Client
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

                    if (!string.IsNullOrEmpty(apiSettings.BaseUrl))
                    {
                        var uriBuilder = new UriBuilder(new Uri(apiSettings.BaseUrl));
                        uriBuilder.Path = string.Concat(uriBuilder.Path.Replace("//", "/").TrimEnd('/'), '/');
                        httpClient.BaseAddress = uriBuilder.Uri;
                    }
                    
                    return new Advocu.AdvocuApiClient(httpClient, apiSettings.AccessToken);
                });

                // You can add other services here if needed
            });
}