using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Advocu
{
    public class CreatePublicSpeakingActivityRequest : IValidatableObject
    {
        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [MaxLength(2000)]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("tags")]
        [MinLength(0)] // min items 0
        public List<string> Tags { get; set; } = new List<string>();

        [Required]
        [Range(1, int.MaxValue)] // minimum 1
        [JsonPropertyName("metrics.attendees")]
        public int Attendees { get; set; }

        [Required]
        [JsonPropertyName("eventFormat")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdvocuEventFormat EventFormat { get; set; }

        // Accepted if eventFormat has value In-Person or Hybrid
        [JsonPropertyName("country")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdvocuCountry? Country { get; set; } // Nullable as it's conditional

        // Accepted if eventFormat has value In-Person or Hybrid
        [JsonPropertyName("inPersonAttendees")]
        [Range(0, int.MaxValue)] // minimum 0
        public int? InPersonAttendees { get; set; } // Nullable as it's conditional

        [Required]
        [JsonPropertyName("activityDate")]
        public string ActivityDate { get; set; } // Pattern YYYY-MM-DD, will format DateTime to this string

        [MaxLength(500)]
        [JsonPropertyName("activityUrl")]
        [Url] // Basic URL validation, regex in image is more specific but UrlAttribute is a good start
        public string ActivityUrl { get; set; }

        [MaxLength(2000)]
        [JsonPropertyName("additionalInfo")]
        public string AdditionalInfo { get; set; }

        [JsonPropertyName("private")] public bool? Private { get; set; } // Nullable bool if it's optional

        /// <summary>
        /// Custom validation to ensure conditional fields are handled correctly before serialization.
        /// This is client-side validation, the API will also validate.
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
            else // In-Person or Hybrid
            {
                if (!Country.HasValue)
                {
                    // This is a stricter client-side rule. The image implies it's "accepted if" rather than "required if".
                    // If the API truly requires it, uncomment this. Otherwise, leave it as nullable on the model.
                    // yield return new ValidationResult(
                    //     $"Country is required when EventFormat is {EventFormat}.",
                    //     new[] { nameof(Country) });
                }

                if (!InPersonAttendees.HasValue)
                {
                    // Similar note as for Country.
                    // yield return new ValidationResult(
                    //     $"InPersonAttendees is required when EventFormat is {EventFormat}.",
                    //     new[] { nameof(InPersonAttendees) });
                }
            }
        }
    }

    public class CreatePublicSpeakingActivityResponse
    {
        
    }
}