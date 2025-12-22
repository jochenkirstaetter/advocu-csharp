using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Advocu
{
    /// <summary>
    /// Represents a request to create a Workshop activity.
    /// </summary>
    public sealed class CreateWorkshopActivityRequest : ActivityRequest, IValidatableObject
    {
        /// <summary>
        /// Gets or sets the number of attendees.
        /// </summary>
        [JsonIgnore]
        public int? Attendees
        {
            get => Metrics.Attendees; 
            set => Metrics.Attendees = Math.Max(value ?? 1, 1);
        }

        /// <summary>
        /// Gets or sets the format of the event.
        /// </summary>
        [Required]
        [JsonPropertyName("eventFormat")]
        [JsonConverter(typeof(JsonStringEnumConverter<AdvocuEventFormat>))]
        public AdvocuEventFormat EventFormat { get; set; } // "Select event format"

        /// <summary>
        /// Gets or sets the country where the event took place (required for In-Person/Hybrid).
        /// </summary>
        // Accepted if eventFormat has value In-Person or Hybrid
        [JsonPropertyName("country")]
        [JsonConverter(typeof(JsonStringEnumConverter<AdvocuCountry>))]
        public AdvocuCountry? Country { get; set; } // Nullable as it's conditional

        /// <summary>
        /// Gets or sets the number of in-person attendees (required for Hybrid events).
        /// </summary>
        // Accepted if eventFormat has value Hybrid
        [JsonPropertyName("inPersonAttendees")]
        [Range(0, int.MaxValue)] // minimum 0
        public int? InPersonAttendees { get; set; } // Nullable as it's conditional

        /// <summary>
        /// Gets or sets the date of the workshop in YYYY-MM-DD format.
        /// </summary>
        [Required]
        [JsonPropertyName("activityDate")]
        public string ActivityDate { get; set; } // "Date of your workshop", Pattern YYYY-MM-DD

        /// <summary>
        /// Gets or sets the URL of the workshop.
        /// </summary>
        [Required]
        [MaxLength(500)]
        [JsonPropertyName("activityUrl")]
        [Url] // Basic URL validation, regex in image is more specific but UrlAttribute is a good start
        public string ActivityUrl { get; set; } // "Share the event link (if it's a GDG event, the gdg.community.dev event URL) or any other relevant link"

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
            else if (EventFormat == AdvocuEventFormat.InPerson && InPersonAttendees.HasValue)
            {
                 yield return new ValidationResult(
                        $"InPersonAttendees is only accepted for Hybrid events. It cannot be provided when EventFormat is {AdvocuEventFormat.InPerson}.",
                        new[] { nameof(InPersonAttendees) });
            }
        }
    }
}