using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Advocu
{
    /// <summary>
    /// Represents a request to create a Product Feedback activity.
    /// </summary>
    public sealed class CreateProductFeedbackActivityRequest : ActivityRequest, IValidatableObject
    {
        /// <summary>
        /// Gets or sets the time spent on feedback in minutes.
        /// </summary>
        [JsonIgnore]
        public int? TimeSpent
        {
            get => Metrics.TimesSpent; 
            set => Metrics.TimesSpent = Math.Max(value ?? 1, 1);
        }

        /// <summary>
        /// Gets or sets the content type of the feedback.
        /// </summary>
        [Required]
        [JsonPropertyName("contentType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdvocuActivityContentType ContentType { get; set; } // Uses the initial ContentType enum

        /// <summary>
        /// Gets or sets the description of the product being reviewed.
        /// </summary>
        [MaxLength(500)]
        [JsonPropertyName("productDescription")]
        public string ProductDescription { get; set; } // "What product was it about?"

        /// <summary>
        /// Gets or sets the date of the activity in YYYY-MM-DD format.
        /// </summary>
        [Required]
        [JsonPropertyName("activityDate")]
        public string ActivityDate { get; set; } // "Participation Date", Pattern YYYY-MM-DD

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}