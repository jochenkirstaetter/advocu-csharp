using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Advocu
{
    public class CreateInteractionWithGooglersActivityRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [MaxLength(2000)]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [Required]
        [JsonPropertyName("format")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdvocuFormat Format { get; set; } // Uses the existing Format enum

        [Required]
        [JsonPropertyName("interactionType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdvocuInteractionType InteractionType { get; set; } // Uses the existing InteractionType enum

        [JsonPropertyName("tags")]
        [MinLength(0)] // min items 0
        public List<string> Tags { get; set; } = new List<string>();

        [Required]
        [Range(1, int.MaxValue)] // minimum 1
        [JsonPropertyName("metrics.timeSpent")]
        public int TimeSpent { get; set; } // "Time spent (in minutes)"

        [Required]
        [JsonPropertyName("activityDate")]
        public string ActivityDate { get; set; } // "Interaction Date", Pattern YYYY-MM-DD

        [MaxLength(2000)]
        [JsonPropertyName("additionalInfo")]
        public string AdditionalInfo { get; set; }

        [MaxLength(2000)]
        [JsonPropertyName("additionalLinks")]
        public string AdditionalLinks { get; set; } // New field for this endpoint

        [JsonPropertyName("private")]
        public bool? Private { get; set; }
    }
}