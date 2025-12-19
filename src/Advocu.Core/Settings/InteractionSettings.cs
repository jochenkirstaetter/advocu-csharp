using System.ComponentModel;
using Spectre.Console.Cli;
using Advocu;

namespace Advocu.NuGet.Settings;

internal class InteractionSettings : AdvocuSettings
{
    [CommandOption("-t|--title <TITLE>")]
    [Description("Title.")]
    public string Title { get; set; } = string.Empty;

    [CommandOption("-d|--description <DESCRIPTION>")]
    [Description("Description.")]
    public string Description { get; set; } = string.Empty;

    [CommandOption("--tags <TAGS>")]
    [Description("Comma-separated list of tags.")]
    public string[] Tags { get; set; } = Array.Empty<string>();

    [CommandOption("--time <MINUTES>")]
    [Description("Time spent in minutes.")]
    public int TimeSpent { get; set; }

    [CommandOption("--format <FORMAT>")]
    [Description("Interaction format.")]
    [DefaultValue(AdvocuFormat.Other)]
    [TypeConverter(typeof(Advocu.NuGet.Parsing.AdvocuEnumConverter<AdvocuFormat>))]
    public AdvocuFormat Format { get; set; }
    
    [CommandOption("--type <TYPE>")]
    [Description("Interaction type.")]
    [DefaultValue(AdvocuInteractionType.OtherInteractionsWithGoogleProductTeams)]
    [TypeConverter(typeof(Advocu.NuGet.Parsing.AdvocuEnumConverter<AdvocuInteractionType>))]
    public AdvocuInteractionType InteractionType { get; set; }

    [CommandOption("--date <DATE>")]
    [Description("Date of activity (YYYY-MM-DD). Defaults to today.")]
    public string? ActivityDate { get; set; }

    [CommandOption("--links <LINKS>")]
    [Description("Additional links.")]
    public string? AdditionalLinks { get; set; }
    
    [CommandOption("--info <INFO>")]
    [Description("Additional info.")]
    public string? AdditionalInfo { get; set; }
}
