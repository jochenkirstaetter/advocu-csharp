using Advocu.Core.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Reflection;

namespace Advocu.Core.Commands;

/// <summary>
/// The root command executed when the CLI runs without arguments.
/// </summary>
internal sealed class RootCommand : AsyncCommand<AdvocuSettings>
{
    /// <summary>
    /// Executes the root command synchronously mapping to async execution.
    /// </summary>
    public Task<int> ExecuteAsync(CommandContext context, AdvocuSettings settings)
    {
        return ExecuteAsync(context, settings, default);
    }

    /// <inheritdoc />
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
