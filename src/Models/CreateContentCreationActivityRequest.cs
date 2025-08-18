using System.ComponentModel.DataAnnotations; // For data annotations if needed for validation
using System.Text.Json.Serialization; // For JSON serialization attributes

namespace Advocu
{
    public class CreateContentCreationActivityRequest
    {
        public CreateContentCreationActivityRequest()
        {
            Metrics = new AdvocuMetrics();
        }
        
        [Required]
        [JsonPropertyName("contentType")]
        [JsonConverter(typeof(JsonStringEnumConverter))] // Serializes enum as string
        public AdvocuActivityContentType ContentType { get; set; }

        [Required]
        [MinLength(3)]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [MaxLength(2000)]
        [JsonPropertyName("description")]
        public string Description { get; set; } // Max length 2000 as per image

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new List<string>(); // Use List<string> for dynamic tags

        [JsonPropertyName("metrics")]
        public AdvocuMetrics Metrics { get; set; }

        [JsonIgnore]
        public int? Readers
        {
            get => Metrics.Readers;
            set => Metrics.Readers = value;
        }

        [JsonPropertyName("activityDate")]
        public string ActivityDate { get; set; } // Pattern yyyy-MM-dd, will format DateTime to this string

        [JsonPropertyName("activityUrl")]
        [Url] // Basic URL validation
        public string ActivityUrl { get; set; }

        [MaxLength(2000)]
        [JsonPropertyName("additionalInfo")]
        public string AdditionalInfo { get; set; } // Max length 2000 as per image

        [JsonPropertyName("private")]
        public bool? Private { get; set; } // Nullable bool if it's optional
    }
}