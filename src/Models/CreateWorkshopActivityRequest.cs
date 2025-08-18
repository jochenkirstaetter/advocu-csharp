using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Advocu
{
    public class CreateWorkshopActivityRequest : IValidatableObject
    {
        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        [JsonPropertyName("title")]
        public string Title { get; set; } // "What was the name of your workshop session?"

        [MaxLength(2000)]
        [JsonPropertyName("description")]
        public string Description { get; set; } // "What was it about?"

        [JsonPropertyName("tags")]
        [MinLength(0)] // min items 0
        public List<string> Tags { get; set; } = new List<string>();

        [JsonPropertyName("metrics")]
        public AdvocuMetrics? Metrics { get; set; }

        [Required]
        [Range(1, int.MaxValue)] // minimum 1
        [JsonPropertyName("metrics.attendees")]
        public int Attendees { get; set; } // "How many people have been trained?"

        [Required]
        [JsonPropertyName("eventFormat")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdvocuEventFormat EventFormat { get; set; } // "Select event format"

        // Accepted if eventFormat has value In-Person or Hybrid
        [JsonPropertyName("country")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdvocuCountry? Country { get; set; } // Nullable as it's conditional

        // Accepted if eventFormat has value Hybrid
        [JsonPropertyName("inPersonAttendees")]
        [Range(0, int.MaxValue)] // minimum 0
        public int? InPersonAttendees { get; set; } // Nullable as it's conditional

        [Required]
        [JsonPropertyName("activityDate")]
        public string ActivityDate { get; set; } // "Date of your workshop", Pattern YYYY-MM-DD

        [MaxLength(500)]
        [JsonPropertyName("activityUrl")]
        [Url] // Basic URL validation, regex in image is more specific but UrlAttribute is a good start
        public string ActivityUrl { get; set; } // "Share the event link (if it's a GDG event, the gdg.community.dev event URL) or any other relevant link"

        [MaxLength(2000)]
        [JsonPropertyName("additionalInfo")]
        public string AdditionalInfo { get; set; }

        [JsonPropertyName("private")]
        public bool? Private { get; set; }

        /// <summary>
        /// Custom validation to ensure conditional fields are handled correctly before serialization.
        /// </summary>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EventFormat == AdvocuEventFormat.Virtual)
            {
                if (Country.HasValue)
                {
                    yield return new ValidationResult(
                        $"Country cannot be provided when EventFormat is {AdvocuEventFormat.Virtual}.",
                        new[] { nameof(Country) });
                }
                if (InPersonAttendees.HasValue)
                {
                    yield return new ValidationResult(
                        $"InPersonAttendees cannot be provided when EventFormat is {AdvocuEventFormat.Virtual}.",
                        new[] { nameof(InPersonAttendees) });
                }
            }
            // The image implies 'inPersonAttendees' is only accepted if eventFormat is 'Hybrid'.
            // If it's In-Person, inPersonAttendees should likely also be valid/required, but the image explicitly states "Hybrid" for that restriction.
            // If the API clarifies that In-Person also accepts it, you might adjust this logic.
            else if (EventFormat == AdvocuEventFormat.In_Person && InPersonAttendees.HasValue)
            {
                 yield return new ValidationResult(
                        $"InPersonAttendees is only accepted for Hybrid events. It cannot be provided when EventFormat is {AdvocuEventFormat.In_Person}.",
                        new[] { nameof(InPersonAttendees) });
            }
        }
    }
}