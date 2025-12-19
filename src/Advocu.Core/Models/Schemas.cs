namespace Advocu;

/// <summary>
/// Represents a schema definition.
/// </summary>
public class Schemas
{
    /// <summary>
    /// Gets or sets the slug.
    /// </summary>
    public string slug { get; set; }
    /// <summary>
    /// Gets or sets the slug title.
    /// </summary>
    public string slugTitle { get; set; }
    /// <summary>
    /// Gets or sets the activity preview.
    /// </summary>
    public ActivityPreview activityPreview { get; set; }
    /// <summary>
    /// Gets or sets the JSON schema.
    /// </summary>
    public string jsonSchema { get; set; }
    /// <summary>
    /// Gets or sets the JSON form UI schema.
    /// </summary>
    public string jsonFormUiSchema { get; set; }
    /// <summary>
    /// Gets or sets the JSON preview UI schema.
    /// </summary>
    public string jsonPreviewUiSchema { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether this is external.
    /// </summary>
    public bool external { get; set; }
    /// <summary>
    /// Gets or sets the activity points.
    /// </summary>
    public object activityPoints { get; set; }
}

/// <summary>
/// Represents an activity preview.
/// </summary>
public class ActivityPreview
{
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string description { get; set; }
    /// <summary>
    /// Gets or sets the icon.
    /// </summary>
    public string icon { get; set; }
    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    public string title { get; set; }
}

