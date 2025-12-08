using System.Text.Json.Serialization;

namespace Advocu;

[JsonConverter(typeof(JsonStringEnumConverter<AdvocuSlug>))]
public enum AdvocuSlug
{
    [JsonStringEnumMemberName("Content Creation")]
    ContentCreation,
    [JsonStringEnumMemberName("Public Speaking")]
    PublicSpeaking,
    Workshop,
    Mentoring,
    [JsonStringEnumMemberName("Product Feedback Given")]
    ProductFeedbackGiven,
    [JsonStringEnumMemberName("Interaction With Googlers")]
    InteractionWithGooglers,
    Stories,
    [JsonStringEnumMemberName("GitHub Repository")]
    GithubRepository,
    [JsonStringEnumMemberName("Youtube Video")]
    YoutubeVideo
}