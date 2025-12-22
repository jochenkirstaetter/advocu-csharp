using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Advocu
{
    /// <summary>
    /// Represents a request to create an Interaction with Googlers activity.
    /// </summary>
    public sealed class CreateInteractionWithGooglersActivityRequest : ActivityRequest, IValidatableObject
    {
        /// <summary>
        /// Gets or sets the time spent on the interaction in minutes.
        /// </summary>
        [JsonIgnore]
        public int? TimeSpent
        {
            get => Metrics.TimesSpent; 
            set => Metrics.TimesSpent = Math.Max(value ?? 1, 1);
        }

        /// <summary>
        /// Gets or sets the format of the interaction.
        /// </summary>
        [Required]
        [JsonPropertyName("format")]
        [JsonConverter(typeof(JsonStringEnumConverter<AdvocuFormat>))]
        public AdvocuFormat Format { get; set; } // Uses the existing Format enum

        /// <summary>
        /// Gets or sets the type of interaction.
        /// </summary>
        [Required]
        [JsonPropertyName("interactionType")]
        [JsonConverter(typeof(JsonStringEnumConverter<AdvocuInteractionType>))]
        public AdvocuInteractionType InteractionType { get; set; } // Uses the existing InteractionType enum

        /// <summary>
        /// Gets or sets the date of the interaction in YYYY-MM-DD format.
        /// </summary>
        [Required]
        [JsonPropertyName("activityDate")]
        public string ActivityDate { get; set; } // "Interaction Date", Pattern YYYY-MM-DD

        /// <summary>
        /// Gets or sets additional links related to the interaction.
        /// </summary>
        [MaxLength(2000)]
        [JsonPropertyName("additionalLinks")]
        public string AdditionalLinks { get; set; } // New field for this endpoint

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}