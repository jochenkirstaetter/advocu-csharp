using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Advocu
{
    /// <summary>
    /// Represents a request to create a Stories activity.
    /// </summary>
    public sealed class CreateStoriesActivityRequest : ActivityRequest, IValidatableObject
    {
        /// <summary>
        /// Gets or sets the impact metric.
        /// </summary>
        [JsonIgnore]
        public int? Impact
        {
            get => Metrics.Impact; 
            set => Metrics.Impact = Math.Max(value ?? 1, 1);
        }

        /// <summary>
        /// Gets or sets the explanation of why this activity is significant.
        /// </summary>
        [MaxLength(2000)]
        [JsonPropertyName("whyIsSignificant")]
        public string WhyIsSignificant { get; set; } // "Tell us more about it and why it is significant"

        /// <summary>
        /// Gets or sets the type of significance.
        /// </summary>
        [Required]
        [JsonPropertyName("significanceType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdvocuSignificanceType SignificanceType { get; set; } // Uses the existing SignificanceType enum

        /// <summary>
        /// Gets or sets the URL associated with the story.
        /// </summary>
        [Required]
        [MaxLength(500)]
        [JsonPropertyName("activityUrl")]
        [Url] // Basic URL validation; the pattern is 'https?:\\/\\/(www\\.)?.*$'
        public string ActivityUrl { get; set; } // "Link"

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}