using System.ComponentModel.DataAnnotations; // For data annotations if needed for validation
using System.Text.Json.Serialization; // For JSON serialization attributes

namespace Advocu
{
    public sealed class CreateContentCreationActivityRequest : ActivityRequest, IValidatableObject
    {
        [JsonIgnore]
        public int? Readers
        {
            get => Metrics.Readers;
            set => Metrics.Readers = Math.Max(value ?? 1, 1);
        }

        [Required]
        [JsonPropertyName("contentType")]
        [JsonConverter(typeof(JsonStringEnumConverter))] // Serializes enum as string
        public AdvocuActivityContentType ContentType { get; set; }

        [JsonPropertyName("activityDate")]
        public string ActivityDate { get; set; } // Pattern yyyy-MM-dd, will format DateTime to this string

        [Required]
        [MaxLength(500)]
        [JsonPropertyName("activityUrl")]
        [Url] // Basic URL validation
        public string ActivityUrl { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}