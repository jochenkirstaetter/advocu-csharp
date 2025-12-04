using Advocu;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Advocu;

public class WorkInProgress
{
// var builder = Host.CreateApplicationBuilder(args);
// builder.Logging.AddConsole(consoleLogOptions => { consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace; });
//
// HashSet<string> subscriptions = [];
// var _minimumLoggingLevel = LoggingLevel.Debug;
//
// builder.Services
//     .AddMcpServer()
//     .WithStdioServerTransport()
//     .WithTools<AdvocuTool>()
//     .WithSubscribeToResourcesHandler(async (ctx, ct) =>
//     {
//         var uri = ctx.Params?.Uri;
//         if (uri is not null)
//         {
//             subscriptions.Add(uri);
//
//             await ctx.Server.SampleAsync([
//                     new ChatMessage(ChatRole.System, "You are a helpful assistant for the Advocu platform."),
//                     new ChatMessage(ChatRole.User, $"Resource {uri}, context: A new subscription was started")
//                 ],
//                 options: new ChatOptions
//                 {
//                     MaxOutputTokens = 100,
//                     Temperature = 0.6f
//                 },
//                 cancellationToken: ct);
//         }
//
//         return new EmptyResult();
//     })
//     .WithUnsubscribeFromResourcesHandler(async (ctx, ct) =>
//     {
//         var uri = ctx.Params?.Uri;
//         if (uri is not null)
//         {
//             subscriptions.Remove(uri);
//         }
//
//         return new EmptyResult();
//     });
//
// ResourceBuilder resource = ResourceBuilder.CreateDefault().AddService("advocu-server");
// builder.Services
//     .AddOpenTelemetry()
//     .WithTracing(b => b.AddSource("*").AddHttpClientInstrumentation().SetResourceBuilder(resource))
//     .WithMetrics(b => b.AddMeter("*").AddHttpClientInstrumentation().SetResourceBuilder(resource))
//     .WithLogging(b => b.SetResourceBuilder(resource))
//     .UseOtlpExporter();
//
// builder.Services.AddSingleton(subscriptions);
//
// builder.Services.AddSingleton<Func<LoggingLevel>>(_ => () => _minimumLoggingLevel);
//
// await builder.Build().RunAsync();
}
