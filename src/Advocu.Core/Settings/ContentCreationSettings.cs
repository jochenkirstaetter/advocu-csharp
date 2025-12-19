using System.ComponentModel;
using Spectre.Console.Cli;
using Advocu;

namespace Advocu.NuGet.Settings;

internal class ContentCreationSettings : AdvocuSettings
{
    [CommandOption("-t|--title <TITLE>")]
    [Description("Title of the activity.")]
    public string Title { get; set; } = string.Empty;

    [CommandOption("-d|--description <DESCRIPTION>")]
    [Description("Description of the activity.")]
    public string Description { get; set; } = string.Empty;

    [CommandOption("--content-type <TYPE>")]
    [Description("Type of content (e.g., Articles, Videos, etc.).")]
    [DefaultValue(AdvocuActivityContentType.Articles)]
    [TypeConverter(typeof(Advocu.NuGet.Parsing.AdvocuEnumConverter<AdvocuActivityContentType>))]
    public AdvocuActivityContentType ContentType { get; set; }

    [CommandOption("--tags <TAGS>")]
    [Description("Comma-separated list of tags.")]
    public string[] Tags { get; set; } = Array.Empty<string>();

    [CommandOption("--readers <COUNT>")]
    [Description("Number of readers/views.")]
    public int Readers { get; set; }

    [CommandOption("--date <DATE>")]
    [Description("Date of activity (YYYY-MM-DD). Defaults to today.")]
    public string? ActivityDate { get; set; }

    [CommandOption("--url <URL>")]
    [Description("URL of the activity.")]
    public string? ActivityUrl { get; set; }

    [CommandOption("--info <INFO>")]
    [Description("Additional info.")]
    public string? AdditionalInfo { get; set; }
}
