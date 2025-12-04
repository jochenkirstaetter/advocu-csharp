using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Advocu
{
    public sealed class CreateMentoringActivityRequest : ActivityRequest, IValidatableObject
    {
        [JsonIgnore]
        public int? Attendees
        {
            get => Metrics.Attendees; 
            set => Metrics.Attendees = Math.Max(value ?? 1, 1);
        }

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
        public string ActivityDate { get; set; } // "Date of your mentoring session", Pattern YYYY-MM-DD

        [Required]
        [MaxLength(500)]
        [JsonPropertyName("activityUrl")]
        [Url] // Basic URL validation, regex in image is more specific but UrlAttribute is a good start
        public string ActivityUrl { get; set; } // "Share the event link (e.g. Accelerator or Solution Challenge website, gdg.community.dev event URL, program website, etc.) or any other relevant link"

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
            // This logic is consistent with the Public Speaking and Workshop models based on the images provided.
            else if (EventFormat == AdvocuEventFormat.InPerson && InPersonAttendees.HasValue)
            {
                 yield return new ValidationResult(
                        $"InPersonAttendees is only accepted for Hybrid events. It cannot be provided when EventFormat is {AdvocuEventFormat.InPerson}.",
                        new[] { nameof(InPersonAttendees) });
            }
        }
    }
}