using System.ComponentModel;
using Spectre.Console.Cli;
using Advocu;

namespace Advocu.NuGet.Settings;

internal class PublicSpeakingSettings : AdvocuSettings
{
    [CommandOption("-t|--title <TITLE>")]
    [Description("Title of the talk.")]
    public string Title { get; set; } = string.Empty;

    [CommandOption("-d|--description <DESCRIPTION>")]
    [Description("Description of the talk.")]
    public string Description { get; set; } = string.Empty;

    [CommandOption("--tags <TAGS>")]
    [Description("Comma-separated list of tags.")]
    public string[] Tags { get; set; } = Array.Empty<string>();

    [CommandOption("--attendees <COUNT>")]
    [Description("Number of attendees.")]
    public int Attendees { get; set; }

    [CommandOption("--in-person-attendees <COUNT>")]
    [Description("Number of in-person attendees (for Hybrid events).")]
    public int? InPersonAttendees { get; set; }

    [CommandOption("--format <FORMAT>")]
    [Description("Event format (InPerson, Virtual, Hybrid).")]
    [DefaultValue(AdvocuEventFormat.InPerson)]
    [TypeConverter(typeof(Advocu.NuGet.Parsing.AdvocuEnumConverter<AdvocuEventFormat>))]
    public AdvocuEventFormat EventFormat { get; set; }
    
    [CommandOption("--country <COUNTRY>")]
    [Description("Country where the event took place.")]
    [TypeConverter(typeof(Advocu.NuGet.Parsing.AdvocuEnumConverter<AdvocuCountry>))]
    public AdvocuCountry Country { get; set; }

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
