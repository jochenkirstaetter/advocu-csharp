using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using Advocu;
using Advocu.Mcp.Shared;
using Advocu.Mcp.SSE;

#if DEBUG
// Check for a specific argument to enable debugging mode
if (args.Contains("--debug"))
{
    // IMPORTANT: Use Console.Error, not Console.Out, or you break the MCP protocol!
    Console.Error.WriteLine($"Process ID: {Environment.ProcessId}");
    Console.Error.WriteLine("Waiting for debugger to attach...");

    // Loop until the debugger is attached
    while (!Debugger.IsAttached)
    {
        Thread.Sleep(100);
    }
    
    Console.Error.WriteLine("Debugger attached! Resuming...");
}
#endif

var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0";

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddMcpServer(options =>
    {
        options.ServerInfo = new()
        {
            Name = "Advocu MCP (SSE)",
            Description = "Advocu MCP Server for SSE",
            Version = version
        };
    })
    .WithHttpTransport()
    .WithPrompts<AdvocuPrompts>()
    .WithResources<AdvocuResources>()
    .WithTools<AdvocuTools>();
// .WithPromptsFromAssembly()
// .WithResourcesFromAssembly()
// .WithToolsFromAssembly();

// Add AdvocuApiClient
builder.Services.AddHttpClient();
builder.Services.AddTransient<AdvocuApiClient>(provider =>
{
    var productHeaderValue = new ProductHeaderValue(
        name: Assembly.GetExecutingAssembly().GetName().Name ?? "Advocu-MCP",
        version: version);
    
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var client = httpClientFactory.CreateClient("AdvocuApi");
    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(productHeaderValue));

    var token = Environment.GetEnvironmentVariable("ADVOCU_API_TOKEN");
    if (string.IsNullOrEmpty(token))
    {
        throw new InvalidOperationException("ADVOCU_API_TOKEN environment variable is not set.");
    }
    
    var baseUrl = Environment.GetEnvironmentVariable("ADVOCU_URL");
    if (!string.IsNullOrEmpty(baseUrl))
    {
        client.BaseAddress = new Uri(baseUrl);
    }

    return new AdvocuApiClient(client, token, options: AdvocuMcpContext.DefaultOptions);
});

var app = builder.Build();

app.MapMcp();

await app.RunAsync();
