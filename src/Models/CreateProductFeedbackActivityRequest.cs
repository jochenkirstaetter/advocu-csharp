using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Advocu
{
    public class CreateProductFeedbackActivityRequest
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
        [JsonPropertyName("contentType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdvocuActivityContentType ContentType { get; set; } // Uses the initial ContentType enum

        [MaxLength(500)]
        [JsonPropertyName("productDescription")]
        public string ProductDescription { get; set; } // "What product was it about?"

        [JsonPropertyName("tags")]
        [MinLength(0)] // min items 0
        public List<string> Tags { get; set; } = new List<string>();

        [Required]
        [Range(1, int.MaxValue)] // minimum 1
        [JsonPropertyName("metrics.timesSpent")]
        public int TimesSpent { get; set; } // "Time spent (in minutes)"

        [Required]
        [JsonPropertyName("activityDate")]
        public string ActivityDate { get; set; } // "Participation Date", Pattern YYYY-MM-DD

        [MaxLength(2000)]
        [JsonPropertyName("additionalInfo")]
        public string AdditionalInfo { get; set; }

        [JsonPropertyName("private")] public bool? Private { get; set; } // "Do you want to make this activity private?"
    }
}