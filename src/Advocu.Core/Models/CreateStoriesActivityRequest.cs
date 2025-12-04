using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Advocu
{
    public sealed class CreateStoriesActivityRequest : ActivityRequest, IValidatableObject
    {
        [JsonIgnore]
        public int? Impact
        {
            get => Metrics.Impact; 
            set => Metrics.Impact = Math.Max(value ?? 1, 1);
        }

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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}