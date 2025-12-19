using System.ComponentModel;
using Spectre.Console.Cli;
using Advocu;

namespace Advocu.NuGet.Settings;

internal class StoriesSettings : AdvocuSettings
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

    [CommandOption("--significant-why <REASON>")]
    [Description("Why is this significant?")]
    public string WhyIsSignificant { get; set; } = string.Empty;
    
    [CommandOption("--significance-type <TYPE>")]
    [Description("Significance type.")]
    [DefaultValue(AdvocuSignificanceType.TechnologyOpenSource)]
    [TypeConverter(typeof(Advocu.NuGet.Parsing.AdvocuEnumConverter<AdvocuSignificanceType>))]
    public AdvocuSignificanceType SignificanceType { get; set; }

    [CommandOption("--impact <COUNT>")]
    [Description("Impact count.")]
    public int Impact { get; set; }

    [CommandOption("--info <INFO>")]
    [Description("Additional info.")]
    public string? AdditionalInfo { get; set; }
}
