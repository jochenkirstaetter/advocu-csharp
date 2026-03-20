using Advocu.Core.Parsing;
using System.ComponentModel;
using Spectre.Console.Cli;

namespace Advocu.Core.Settings;

/// <summary>
/// Settings for the mentoring command.
/// </summary>
internal class MentoringSettings : AdvocuSettings
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

    [CommandOption("--attendees <COUNT>")]
    [Description("Number of mentees.")]
    public int Attendees { get; set; }

    [CommandOption("--format <FORMAT>")]
    [Description("Event format (InPerson, Virtual, Hybrid).")]
    [DefaultValue(AdvocuEventFormat.InPerson)]
    [TypeConverter(typeof(AdvocuEnumConverter<AdvocuEventFormat>))]
    public AdvocuEventFormat EventFormat { get; set; }

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
