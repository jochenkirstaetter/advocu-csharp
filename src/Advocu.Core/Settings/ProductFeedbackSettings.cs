using System.ComponentModel;
using Spectre.Console.Cli;
using Advocu;

namespace Advocu.NuGet.Settings;

internal class ProductFeedbackSettings : AdvocuSettings
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

    [CommandOption("--content-type <TYPE>")]
    [Description("Content type.")]
    [DefaultValue(AdvocuActivityContentType.EarlyAccessProgram)]
    [TypeConverter(typeof(Advocu.NuGet.Parsing.AdvocuEnumConverter<AdvocuActivityContentType>))]
    public AdvocuActivityContentType ContentType { get; set; }
    
    [CommandOption("--product-desc <DESC>")]
    [Description("Product description.")]
    public string ProductDescription { get; set; } = string.Empty;

    [CommandOption("--time <MINUTES>")]
    [Description("Time spent in minutes.")]
    public int TimeSpent { get; set; }

    [CommandOption("--date <DATE>")]
    [Description("Date of activity (YYYY-MM-DD). Defaults to today.")]
    public string? ActivityDate { get; set; }

    [CommandOption("--info <INFO>")]
    [Description("Additional info.")]
    public string? AdditionalInfo { get; set; }
}
