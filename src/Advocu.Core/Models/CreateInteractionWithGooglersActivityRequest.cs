using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Advocu
{
    public sealed class CreateInteractionWithGooglersActivityRequest : ActivityRequest, IValidatableObject
    {
        [JsonIgnore]
        public int? TimeSpent
        {
            get => Metrics.TimesSpent; 
            set => Metrics.TimesSpent = Math.Max(value ?? 1, 1);
        }

        [Required]
        [JsonPropertyName("format")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdvocuFormat Format { get; set; } // Uses the existing Format enum

        [Required]
        [JsonPropertyName("interactionType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdvocuInteractionType InteractionType { get; set; } // Uses the existing InteractionType enum

        [Required]
        [JsonPropertyName("activityDate")]
        public string ActivityDate { get; set; } // "Interaction Date", Pattern YYYY-MM-DD

        [MaxLength(2000)]
        [JsonPropertyName("additionalLinks")]
        public string AdditionalLinks { get; set; } // New field for this endpoint

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}