using Spectre.Console;
using Spectre.Console.Cli;
using Advocu;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Advocu.NuGet.Commands;

internal class ListSettings : CommandSettings
{
}

internal class ListTagsCommand : Command<ListSettings>
{
    public override int Execute(CommandContext context, ListSettings settings, System.Threading.CancellationToken cancellationToken)
    {
        return EnumLister.List<AdvocuTag>("Tags");
    }
}

internal class ListContentTypesCommand : Command<ListSettings>
{
    public override int Execute(CommandContext context, ListSettings settings, System.Threading.CancellationToken cancellationToken)
    {
        return EnumLister.List<AdvocuActivityContentType>("Content Types");
    }
}

internal class ListCountriesCommand : Command<ListSettings>
{
    public override int Execute(CommandContext context, ListSettings settings, System.Threading.CancellationToken cancellationToken)
    {
        return EnumLister.List<AdvocuCountry>("Countries");
    }
}

internal class ListEventFormatsCommand : Command<ListSettings>
{
    public override int Execute(CommandContext context, ListSettings settings, System.Threading.CancellationToken cancellationToken)
    {
        return EnumLister.List<AdvocuEventFormat>("Event Formats");
    }
}

internal class ListInteractionTypesCommand : Command<ListSettings>
{
    public override int Execute(CommandContext context, ListSettings settings, System.Threading.CancellationToken cancellationToken)
    {
        return EnumLister.List<AdvocuInteractionType>("Interaction Types");
    }
}

internal class ListSignificanceTypesCommand : Command<ListSettings>
{
    public override int Execute(CommandContext context, ListSettings settings, System.Threading.CancellationToken cancellationToken)
    {
        return EnumLister.List<AdvocuSignificanceType>("Significance Types");
    }
}

// Extension to use static helper from main ListTagsCommand (or move helper to separate class)
// For simplicity in this file, I'll make the helper internal static in a shared class or just duplicate/refactor.
// Refactoring for cleanliness:
internal static class EnumLister
{
    public static int List<T>(string title) where T : struct, Enum
    {
        var table = new Table();
        table.AddColumn("Enum Name");
        table.AddColumn("Display Name (API Value)");

        foreach (var value in Enum.GetValues<T>())
        {
            var name = value.ToString();
            var memberInfo = typeof(T).GetMember(name).FirstOrDefault();
            var displayName = name;

            if (memberInfo != null)
            {
                var attr = memberInfo.GetCustomAttribute<JsonStringEnumMemberNameAttribute>();
                if (attr != null)
                {
                    displayName = attr.Name;
                }
            }
            
            table.AddRow($"[cyan]{name}[/]", displayName);
        }

        AnsiConsole.MarkupLine($"[bold underline]Available {title}[/]");
        AnsiConsole.Write(table);
        return 0;
    }
}
