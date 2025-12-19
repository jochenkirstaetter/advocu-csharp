using System.Diagnostics.CodeAnalysis;
using Advocu.NuGet.Settings;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Advocu.NuGet.Commands;

internal class DefaultCommand : AsyncCommand<AdvocuSettings>
{
    public override Task<int> ExecuteAsync(CommandContext context, AdvocuSettings settings, CancellationToken cancellationToken)
    {
        // This command is executed when no other command is tailored.
        AnsiConsole.MarkupLine("[yellow]Welcome to Advocu CLI![/]");
        AnsiConsole.MarkupLine("Run [blue]advocu --help[/] to see available commands.");
        return Task.FromResult(0);
    }
}
