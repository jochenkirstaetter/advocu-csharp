using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Advocu
{
    public sealed class CreateProductFeedbackActivityRequest : ActivityRequest, IValidatableObject
    {
        [JsonIgnore]
        public int? TimeSpent
        {
            get => Metrics.TimesSpent; 
            set => Metrics.TimesSpent = Math.Max(value ?? 1, 1);
        }

        [Required]
        [JsonPropertyName("contentType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdvocuActivityContentType ContentType { get; set; } // Uses the initial ContentType enum

        [MaxLength(500)]
        [JsonPropertyName("productDescription")]
        public string ProductDescription { get; set; } // "What product was it about?"

        [Required]
        [JsonPropertyName("activityDate")]
        public string ActivityDate { get; set; } // "Participation Date", Pattern YYYY-MM-DD

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}