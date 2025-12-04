using System.Text.Json.Serialization;

namespace Advocu;

[JsonConverter(typeof(JsonStringEnumConverter<AdvocuSlug>))]
public enum AdvocuSlug
{
    ContentCreation,
    PublicSpeaking,
    Workshop,
    Mentoring,
    ProductFeedbackGiven,
    InteractionWithGooglers,
    Stories,
    GithubRepository,
    YoutubeVideo
}