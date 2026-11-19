using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Advocu
{ 
    /// <summary>
    /// Client for interacting with the Advocu API.
    /// </summary>
    public class AdvocuApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerWriteOptions;
        private readonly JsonSerializerOptions _jsonSerializerPatchOptions;
        private readonly JsonSerializerOptions _jsonSerializerReadOptions;

        // Base URL from the image
        private const string BaseUrl = "https://api.advocu.com/personal-api/v1/gde/";

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvocuApiClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client to use for requests.</param>
        /// <param name="accessToken">The access token for authentication.</param>
        /// <param name="options">The options of JSON serialization.</param>
        public AdvocuApiClient(HttpClient httpClient, 
            string accessToken,
            JsonSerializerOptions? options = null)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            ArgumentException.ThrowIfNullOrEmpty(nameof(accessToken));

            _httpClient.BaseAddress ??= new Uri(BaseUrl);

            // Set default headers for all requests from this client
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            _jsonSerializerWriteOptions = options ?? new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _jsonSerializerPatchOptions = new JsonSerializerOptions(_jsonSerializerWriteOptions)
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            _jsonSerializerReadOptions = options ?? new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <summary>
        /// Posts a new activity draft for content creation to the Advocu platform.
        /// </summary>
        /// <param name="request">The request payload for creating an activity draft.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A response object indicating the success or failure of the operation.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if client-side validation fails.</exception>
        public async Task<ActivityResponse> PostContentCreationActivityAsync(CreateContentCreationActivityRequest request, CancellationToken cancellationToken = default)
        {
            const string endpoint = $"activity-drafts/content-creation";
            ValidateRequest(request);
            return await PostJsonAsync<CreateContentCreationActivityRequest, ActivityResponse>(endpoint, request, cancellationToken);
        }

        /// <summary>
        /// Posts a new public speaking activity draft to the Advocu platform.
        /// </summary>
        /// <param name="request">The request payload for creating a public speaking activity draft.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A response object indicating the success or failure of the operation.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if client-side validation fails.</exception>
        public async Task<ActivityResponse> PostPublicSpeakingActivityAsync(CreatePublicSpeakingActivityRequest request, CancellationToken cancellationToken = default)
        {
            const string endpoint = $"activity-drafts/public-speaking";
            ValidateRequest(request);
            return await PostJsonAsync<CreatePublicSpeakingActivityRequest, ActivityResponse>(endpoint, request, cancellationToken);
        }
        
        /// <summary>
        /// Posts a new workshop activity draft to the Advocu platform.
        /// </summary>
        /// <param name="request">The request payload for creating a workshop activity draft.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A response object indicating the success or failure of the operation.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if client-side validation fails.</exception>
        public async Task<ActivityResponse> PostWorkshopActivityAsync(CreateWorkshopActivityRequest request, CancellationToken cancellationToken = default)
        {
            const string endpoint = $"activity-drafts/workshop";
            ValidateRequest(request);
            return await PostJsonAsync<CreateWorkshopActivityRequest, ActivityResponse>(endpoint, request, cancellationToken);
        }

        /// <summary>
        /// Posts a new mentoring activity draft to the Advocu platform.
        /// </summary>
        /// <param name="request">The request payload for creating a mentoring activity draft.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A response object indicating the success or failure of the operation.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if client-side validation fails.</exception>
        public async Task<ActivityResponse> PostMentoringActivityAsync(CreateMentoringActivityRequest request, CancellationToken cancellationToken = default)
        {
            const string endpoint = $"activity-drafts/mentoring";
            ValidateRequest(request);
            return await PostJsonAsync<CreateMentoringActivityRequest, ActivityResponse>(endpoint, request, cancellationToken);
        }

        /// <summary>
        /// Posts a new product feedback activity draft to the Advocu platform.
        /// </summary>
        /// <param name="request">The request payload for creating a product feedback activity draft.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A response object indicating the success or failure of the operation.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if client-side validation fails.</exception>
        public async Task<ActivityResponse> PostProductFeedbackActivityAsync(CreateProductFeedbackActivityRequest request, CancellationToken cancellationToken = default)
        {
            const string endpoint = $"activity-drafts/product-feedback-given";
            ValidateRequest(request);
            return await PostJsonAsync<CreateProductFeedbackActivityRequest, ActivityResponse>(endpoint, request, cancellationToken);
        }

        /// <summary>
        /// Posts a new interaction with Googlers activity draft to the Advocu platform.
        /// </summary>
        /// <param name="request">The request payload for creating an interaction with Googlers activity draft.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A response object indicating the success or failure of the operation.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if client-side validation fails.</exception>
        public async Task<ActivityResponse> PostInteractionWithGooglersActivityAsync(CreateInteractionWithGooglersActivityRequest request, CancellationToken cancellationToken = default)
        {
            const string endpoint = $"activity-drafts/interaction-with-googlers";
            ValidateRequest(request);
            return await PostJsonAsync<CreateInteractionWithGooglersActivityRequest, ActivityResponse>(endpoint, request, cancellationToken);
        }

        /// <summary>
        /// Posts a new story activity draft to the Advocu platform.
        /// </summary>
        /// <param name="request">The request payload for creating a story activity draft.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A response object indicating the success or failure of the operation.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if client-side validation fails.</exception>
        public async Task<ActivityResponse> PostStoriesActivityAsync(CreateStoriesActivityRequest request, CancellationToken cancellationToken = default)
        {
            const string endpoint = $"activity-drafts/stories";
            ValidateRequest(request);
            return await PostJsonAsync<CreateStoriesActivityRequest, ActivityResponse>(endpoint, request, cancellationToken);
        }

        /// <summary>
        /// Retrieves a paginated list of activities sorted by submission date in ascending order.
        /// </summary>
        /// <param name="query">Optional query parameters for filtering and pagination.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A paginated response containing the matching activities.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if query validation fails.</exception>
        public async Task<ActivitiesResponse> GetActivitiesAsync(GetActivitiesQuery? query = null, CancellationToken cancellationToken = default)
        {
            var endpoint = "activities";

            if (query is not null)
            {
                ValidateRequest(query);

                var queryParts = new List<string>();
                if (query.From.HasValue)
                {
                    queryParts.Add($"from={Uri.EscapeDataString(query.From.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture))}");
                }
                if (query.To.HasValue)
                {
                    queryParts.Add($"to={Uri.EscapeDataString(query.To.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture))}");
                }
                if (query.UpdatedAfter.HasValue)
                {
                    queryParts.Add($"updatedAfter={Uri.EscapeDataString(query.UpdatedAfter.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture))}");
                }
                if (query.Page.HasValue)
                {
                    queryParts.Add($"page={query.Page.Value}");
                }
                if (query.Size.HasValue)
                {
                    queryParts.Add($"size={query.Size.Value}");
                }

                if (queryParts.Count > 0)
                {
                    endpoint = $"{endpoint}?{string.Join("&", queryParts)}";
                }
            }

            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, endpoint);
            return await SendJsonAsync<ActivitiesResponse>(requestMessage, cancellationToken);
        }

        /// <summary>
        /// Partially updates an existing activity on the Advocu platform.
        /// </summary>
        /// <param name="activityId">The identifier of the activity to update.</param>
        /// <param name="request">The partial update request payload.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>The updated activity.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="activityId"/> is null or whitespace.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="request"/> is null.</exception>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if client-side validation fails.</exception>
        public async Task<ActivityItem> PatchActivityAsync(string activityId, PatchActivityRequest request, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(activityId);
            ArgumentNullException.ThrowIfNull(request);

            ValidateRequest(request);
            ValidateRequest(request.Data);
            if (request.Data.Metrics is not null)
            {
                ValidateRequest(request.Data.Metrics);
            }

            var endpoint = $"activities/{Uri.EscapeDataString(activityId)}";
            var jsonRequest = JsonSerializer.Serialize(request, _jsonSerializerPatchOptions);
            using var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            using var requestMessage = new HttpRequestMessage(HttpMethod.Patch, endpoint) { Content = content };

            return await SendJsonAsync<ActivityItem>(requestMessage, cancellationToken);
        }
 
        /// <summary>
        /// Validates the given request object using data annotations.
        /// </summary>
        /// <param name="request">The request object to validate.</param>
        /// <exception cref="ValidationException"></exception>
        private static void ValidateRequest(object request)
        {
            // Client-side validation before sending the request
            var validationContext = new ValidationContext(request, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(request, validationContext, validationResults, validateAllProperties: true))
            {
                // This handles standard DataAnnotations as well as the custom Validate method
                var errorMessage = new StringBuilder("Validation errors occurred:\n");
                foreach (var validationResult in validationResults)
                {
                    errorMessage.AppendLine($"- {validationResult.ErrorMessage}");
                }
                throw new ValidationException(errorMessage.ToString());
            }
        }
        
        /// <summary>
        /// Generic helper method to post JSON data and deserialize the response.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request payload.</typeparam>
        /// <typeparam name="TResponse">The type of the response to deserialize.</typeparam>
        /// <param name="endpoint">The API endpoint to post to.</param>
        /// <param name="requestPayload">The payload to send in the request body.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The deserialized response object.</returns>
        private async Task<TResponse> PostJsonAsync<TRequest, TResponse>(string endpoint, TRequest requestPayload, CancellationToken cancellationToken = default)
        {
            var jsonRequest = JsonSerializer.Serialize(requestPayload, _jsonSerializerWriteOptions);
            using var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, endpoint) { Content = content };
            return await SendJsonAsync<TResponse>(requestMessage, cancellationToken);
        }

        private async Task<TResponse> SendJsonAsync<TResponse>(HttpRequestMessage requestMessage, CancellationToken cancellationToken = default)
        {
            using HttpResponseMessage response = await _httpClient.SendAsync(requestMessage, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
                return JsonSerializer.Deserialize<TResponse>(jsonResponse, _jsonSerializerReadOptions)!;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                await Console.Error.WriteLineAsync($"API Error: {response.StatusCode} - {errorContent}");

                if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests) // 429
                {
                    throw new HttpRequestException($"Rate limit exceeded. Please wait before retrying. Details: {errorContent}", null, response.StatusCode);
                }

                response.EnsureSuccessStatusCode();
                return default(TResponse)!; // Should not reach here
            }
        }
    }
}