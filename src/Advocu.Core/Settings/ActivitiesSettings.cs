using Advocu.Core.Parsing;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace Advocu.Core.Settings;

/// <summary>
/// Settings for the <c>activities</c> command (<c>GET /activities</c>).
/// </summary>
internal sealed class GetActivitiesSettings : AdvocuSettings
{
    [CommandOption("--from <TIMESTAMP>")]
    [Description("Filter by submission date from (inclusive, ISO-8601 UTC, e.g. 2026-01-01T00:00:00Z).")]
    public string? From { get; set; }

    [CommandOption("--to <TIMESTAMP>")]
    [Description("Filter by submission date to (exclusive, ISO-8601 UTC, e.g. 2026-01-01T00:00:00Z).")]
    public string? To { get; set; }

    [CommandOption("--updated-after <TIMESTAMP>")]
    [Description("Filter activities updated after this timestamp (ISO-8601 UTC, e.g. 2026-01-01T00:00:00Z).")]
    public string? UpdatedAfter { get; set; }

    [CommandOption("--page <PAGE>")]
    [Description("Zero-based page number (default: 0).")]
    public int? Page { get; set; }

    [CommandOption("--size <SIZE>")]
    [Description("Page size from 1 to 100 (default: 10).")]
    public int? Size { get; set; }

    [CommandOption("--json")]
    [Description("Output the raw JSON response.")]
    public bool Json { get; set; }
}

/// <summary>
/// Settings for the <c>patch</c> command (<c>PATCH /activities/{activityId}</c>).
/// </summary>
internal sealed class PatchActivitySettings : AdvocuSettings
{
    [CommandArgument(0, "<ACTIVITY_ID>")]
    [Description("The ID of the activity to update.")]
    public string ActivityId { get; set; } = string.Empty;

    [CommandOption("-t|--title <TITLE>")]
    [Description("Updated title (3 to 200 characters).")]
    public string? Title { get; set; }

    [CommandOption("-d|--description <DESCRIPTION>")]
    [Description("Updated description (up to 2000 characters).")]
    public string? Description { get; set; }

    [CommandOption("--content-type <TYPE>")]
    [Description("Updated content type (for content creation or product feedback).")]
    [TypeConverter(typeof(AdvocuEnumConverter<AdvocuActivityContentType>))]
    public AdvocuActivityContentType? ContentType { get; set; }

    [CommandOption("--product-description <TEXT>")]
    [Description("Updated product description (up to 500 characters, for product feedback).")]
    public string? ProductDescription { get; set; }

    [CommandOption("--format <FORMAT>")]
    [Description("Updated interaction format (for interaction with Googlers).")]
    [TypeConverter(typeof(AdvocuEnumConverter<AdvocuFormat>))]
    public AdvocuFormat? Format { get; set; }

    [CommandOption("--interaction-type <TYPE>")]
    [Description("Updated interaction type (for interaction with Googlers).")]
    [TypeConverter(typeof(AdvocuEnumConverter<AdvocuInteractionType>))]
    public AdvocuInteractionType? InteractionType { get; set; }

    [CommandOption("--event-format <FORMAT>")]
    [Description("Updated event format (In-Person, Virtual, Hybrid).")]
    [TypeConverter(typeof(AdvocuEnumConverter<AdvocuEventFormat>))]
    public AdvocuEventFormat? EventFormat { get; set; }

    [CommandOption("--country <COUNTRY>")]
    [Description("Updated country (In-Person or Hybrid events).")]
    [TypeConverter(typeof(AdvocuEnumConverter<AdvocuCountry>))]
    public AdvocuCountry? Country { get; set; }

    [CommandOption("--in-person-attendees <COUNT>")]
    [Description("Updated in-person attendee count (Hybrid events).")]
    public int? InPersonAttendees { get; set; }

    [CommandOption("--why-significant <TEXT>")]
    [Description("Updated significance explanation (stories).")]
    public string? WhyIsSignificant { get; set; }

    [CommandOption("--significance-type <TYPE>")]
    [Description("Updated significance type (stories).")]
    [TypeConverter(typeof(AdvocuEnumConverter<AdvocuSignificanceType>))]
    public AdvocuSignificanceType? SignificanceType { get; set; }

    [CommandOption("--tags <TAGS>")]
    [Description("Updated tags (comma-separated or repeated option).")]
    public string[]? Tags { get; set; }

    [CommandOption("--readers <COUNT>")]
    [Description("Updated reader count (content creation).")]
    public int? Readers { get; set; }

    [CommandOption("--attendees <COUNT>")]
    [Description("Updated attendee count (mentoring, public speaking, workshop).")]
    public int? Attendees { get; set; }

    [CommandOption("--time-spent <MINUTES>")]
    [Description("Updated time spent in minutes (product feedback, interaction with Googlers).")]
    public int? TimeSpent { get; set; }

    [CommandOption("--impact <COUNT>")]
    [Description("Updated impact metric (stories).")]
    public int? Impact { get; set; }

    [CommandOption("--date <YYYY-MM-DD>")]
    [Description("Updated activity date (YYYY-MM-DD).")]
    public string? ActivityDate { get; set; }

    [CommandOption("--activity-url <URL>")]
    [Description("Updated activity URL.")]
    public string? ActivityUrl { get; set; }

    [CommandOption("--additional-info <TEXT>")]
    [Description("Updated additional information (up to 2000 characters).")]
    public string? AdditionalInfo { get; set; }

    [CommandOption("--additional-links <TEXT>")]
    [Description("Updated additional links (up to 2000 characters, interaction with Googlers).")]
    public string? AdditionalLinks { get; set; }

    [CommandOption("--set-private <BOOL>")]
    [Description("Updated privacy flag (true or false).")]
    public bool? SetPrivate { get; set; }

    [CommandOption("--json")]
    [Description("Output the raw JSON response.")]
    public bool Json { get; set; }
}
