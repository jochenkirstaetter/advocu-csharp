namespace Advocu.NuGet.Models;

/// <summary>
/// Represents a draft of an activity to be submitted to Advocu.
/// </summary>
internal sealed class ActivityDraft
{
    /// <summary>
    /// Gets or sets the type of the activity.
    /// </summary>
    public string? ActivityType { get; set; }
    
    // Common fields
    /// <summary>
    /// Gets or sets the title of the activity.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the activity.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the date of the activity.
    /// </summary>
    public DateTime? ActivityDate { get; set; }

    /// <summary>
    /// Gets or sets the URL related to the activity.
    /// </summary>
    public string? ActivityUrl { get; set; }

    /// <summary>
    /// Gets or sets additional information about the activity.
    /// </summary>
    public string? AdditionalInfo { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the activity is private.
    /// </summary>
    public bool? Private { get; set; }

    // Specific fields (Content Creation)
    /// <summary>
    /// Gets or sets the content type (for Content Creation activities).
    /// </summary>
    public string? ContentType { get; set; }

    /// <summary>
    /// Gets or sets the number of readers (for Content Creation activities).
    /// </summary>
    public int? Readers { get; set; }
    
    // Specific fields (Public Speaking/Workshop)
    /// <summary>
    /// Gets or sets the event name (for Public Speaking/Workshop activities).
    /// </summary>
    public string? EventName { get; set; }

    /// <summary>
    /// Gets or sets the event URL (for Public Speaking/Workshop activities).
    /// </summary>
    public string? EventUrl { get; set; }

    /// <summary>
    /// Gets or sets the country where the activity took place.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the city where the activity took place.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the event format (e.g., InPerson, Virtual).
    /// </summary>
    public string? EventFormat { get; set; }

    /// <summary>
    /// Gets or sets the number of attendees.
    /// </summary>
    public int? Attendees { get; set; }
    
    // Specific fields (Mentoring)
    /// <summary>
    /// Gets or sets the mentoring focus area.
    /// </summary>
    public string? MentoringFocus { get; set; }

    /// <summary>
    /// Gets or sets the number of mentees.
    /// </summary>
    public int? Mentees { get; set; }

    /// <summary>
    /// Gets or sets the number of hours spent.
    /// </summary>
    public int? Hours { get; set; }

    // Specific fields (Product Feedback)
    /// <summary>
    /// Gets or sets the product team receiving the feedback.
    /// </summary>
    public string? ProductTeam { get; set; }

    /// <summary>
    /// Gets or sets the URL for the feedback.
    /// </summary>
    public string? FeedbackUrl { get; set; }

    // Specific fields (Interaction)
    /// <summary>
    /// Gets or sets the type of interaction with Googlers.
    /// </summary>
    public string? InteractionType { get; set; }

    /// <summary>
    /// Gets or sets the names or details of Googlers involved.
    /// </summary>
    public string? Googlers { get; set; }

    // Specific fields (Stories)
    /// <summary>
    /// Gets or sets the type of success story.
    /// </summary>
    public string? StoryType { get; set; }

    /// <summary>
    /// Gets or sets the significance of the story.
    /// </summary>
    public string? Significance { get; set; }

    /// <summary>
    /// Gets or sets the list of tags associated with the activity.
    /// </summary>
    public List<string> Tags { get; set; } = new();

    // Helper to check if minimum required fields are present
    /// <summary>
    /// Gets a value indicating whether the draft has the minimum required fields (Type and Title).
    /// </summary>
    public bool IsValid => !string.IsNullOrEmpty(ActivityType) && !string.IsNullOrEmpty(Title);
}
