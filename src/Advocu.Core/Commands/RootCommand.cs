using Advocu.NuGet.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Reflection;

namespace Advocu.NuGet.Commands;

internal sealed class RootCommand : AsyncCommand<AdvocuSettings>
{
    public Task<int> ExecuteAsync(CommandContext context, AdvocuSettings settings)
    {
        return ExecuteAsync(context, settings, default);
    }

    public override Task<int> ExecuteAsync(CommandContext context, AdvocuSettings settings, CancellationToken cancellationToken)
    {
        var versionString = Assembly.GetEntryAssembly()?
                            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                            .InformationalVersion
                            .ToString();

        AnsiConsole.MarkupLine($"[bold deepskyblue1]Advocu C#[/] - v{versionString}");
        AnsiConsole.MarkupLine("[italic]An Advocu client for .NET[/]");
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("Run [blue]advocu --help[/] to see available commands.");
        return Task.FromResult(0);
    }
}
