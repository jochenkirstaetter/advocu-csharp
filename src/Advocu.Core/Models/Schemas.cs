namespace Advocu;

public class Schemas
{
    public string slug { get; set; }
    public string slugTitle { get; set; }
    public ActivityPreview activityPreview { get; set; }
    public string jsonSchema { get; set; }
    public string jsonFormUiSchema { get; set; }
    public string jsonPreviewUiSchema { get; set; }
    public bool external { get; set; }
    public object activityPoints { get; set; }
}

public class ActivityPreview
{
    public string description { get; set; }
    public string icon { get; set; }
    public string title { get; set; }
}

