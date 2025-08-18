using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Advocu
{
    public class CreateStoryActivityRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        [JsonPropertyName("title")]
        public string Title { get; set; } // "Title of the story"

        [MaxLength(2000)]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [MaxLength(2000)]
        [JsonPropertyName("whyIsSignificant")]
        public string WhyIsSignificant { get; set; } // "Tell us more about it and why it is significant"

        [Required]
        [JsonPropertyName("significanceType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdvocuSignificanceType SignificanceType { get; set; } // Uses the existing SignificanceType enum

        [Required]
        [MaxLength(500)]
        [JsonPropertyName("activityUrl")]
        [Url] // Basic URL validation; the pattern is 'https?:\\/\\/(www\\.)?.*$'
        public string ActivityUrl { get; set; } // "Link"

        [JsonPropertyName("tags")]
        [MinLength(0)] // min items 0
        public List<string> Tags { get; set; } = new List<string>();

        [Required]
        [Range(1, int.MaxValue)] // minimum 1
        [JsonPropertyName("metrics.impact")]
        public int Impact { get; set; } // "What was your impact in views, reads, attendees, etc.? Add a number below:"

        [MaxLength(2000)]
        [JsonPropertyName("additionalInfo")]
        public string AdditionalInfo { get; set; }

        [JsonPropertyName("private")]
        public bool? Private { get; set; }
    }
}