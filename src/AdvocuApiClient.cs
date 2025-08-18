using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Advocu
{ 
    public class AdvocuApiClient
    {
        private readonly HttpClient _httpClient;

        // Base URL from the image
        private const string BaseUrl = "https://api.advocu.com/personal-api/v1/gde";

        public AdvocuApiClient(HttpClient httpClient, string accessToken)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            ArgumentNullException.ThrowIfNullOrEmpty(nameof(accessToken));

            // Set default headers for all requests from this client
            //if (_httpClient.BaseAddress is null)
            //    _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        /// <summary>
        /// Posts a new activity draft for content creation to the Advocu platform.
        /// </summary>
        /// <param name="request">The request payload for creating an activity draft.</param>
        /// <returns>A response object indicating the success or failure of the operation.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if client-side validation fails.</exception>
        public async Task<CreateContentCreationActivityResponse> PostContentCreationActivityAsync(CreateContentCreationActivityRequest request)
        {
            const string endpoint = $"{BaseUrl}/activity-drafts/content-creation";
            ValidateRequest(request);
            return await PostJsonAsync<CreateContentCreationActivityRequest, CreateContentCreationActivityResponse>(endpoint, request);
        }

        /// <summary>
        /// Posts a new public speaking activity draft to the Advocu platform.
        /// </summary>
        /// <param name="request">The request payload for creating a public speaking activity draft.</param>
        /// <returns>A response object indicating the success or failure of the operation.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if client-side validation fails.</exception>
        public async Task<CreateContentCreationActivityResponse> PostPublicSpeakingActivityAsync(CreatePublicSpeakingActivityRequest request)
        {
            const string endpoint = $"{BaseUrl}/activity-drafts/public-speaking";
            ValidateRequest(request);
            return await PostJsonAsync<CreatePublicSpeakingActivityRequest, CreateContentCreationActivityResponse>(endpoint, request);
        }
        
        /// <summary>
        /// Posts a new workshop activity draft to the Advocu platform.
        /// </summary>
        /// <param name="request">The request payload for creating a workshop activity draft.</param>
        /// <returns>A response object indicating the success or failure of the operation.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if client-side validation fails.</exception>
        public async Task<CreateContentCreationActivityResponse> PostWorkshopActivityAsync(CreateWorkshopActivityRequest request)
        {
            const string endpoint = $"{BaseUrl}/activity-drafts/workshop";
            ValidateRequest(request);
            return await PostJsonAsync<CreateWorkshopActivityRequest, CreateContentCreationActivityResponse>(endpoint, request);
        }

        /// <summary>
        /// Posts a new mentoring activity draft to the Advocu platform.
        /// </summary>
        /// <param name="request">The request payload for creating a mentoring activity draft.</param>
        /// <returns>A response object indicating the success or failure of the operation.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if client-side validation fails.</exception>
        public async Task<CreateContentCreationActivityResponse> PostMentoringActivityAsync(CreateMentoringActivityRequest request)
        {
            const string endpoint = $"{BaseUrl}/activity-drafts/mentoring";
            ValidateRequest(request);
            return await PostJsonAsync<CreateMentoringActivityRequest, CreateContentCreationActivityResponse>(endpoint, request);
        }

        /// <summary>
        /// Posts a new product feedback activity draft to the Advocu platform.
        /// </summary>
        /// <param name="request">The request payload for creating a product feedback activity draft.</param>
        /// <returns>A response object indicating the success or failure of the operation.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if client-side validation fails.</exception>
        public async Task<CreateContentCreationActivityResponse> PostProductFeedbackActivityAsync(CreateProductFeedbackActivityRequest request)
        {
            const string endpoint = $"{BaseUrl}/activity-drafts/product-feedback-given";
            ValidateRequest(request);
            return await PostJsonAsync<CreateProductFeedbackActivityRequest, CreateContentCreationActivityResponse>(endpoint, request);
        }

        /// <summary>
        /// Posts a new interaction with Googlers activity draft to the Advocu platform.
        /// </summary>
        /// <param name="request">The request payload for creating an interaction with Googlers activity draft.</param>
        /// <returns>A response object indicating the success or failure of the operation.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if client-side validation fails.</exception>
        public async Task<CreateContentCreationActivityResponse> PostInteractionWithGooglersActivityAsync(CreateInteractionWithGooglersActivityRequest request)
        {
            const string endpoint = $"{BaseUrl}/activity-drafts/interaction-with-googlers";
            ValidateRequest(request);
            return await PostJsonAsync<CreateInteractionWithGooglersActivityRequest, CreateContentCreationActivityResponse>(endpoint, request);
        }

        /// <summary>
        /// Posts a new story activity draft to the Advocu platform.
        /// </summary>
        /// <param name="request">The request payload for creating a story activity draft.</param>
        /// <returns>A response object indicating the success or failure of the operation.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails or returns a non-success status code.</exception>
        /// <exception cref="ValidationException">Thrown if client-side validation fails.</exception>
        public async Task<CreateContentCreationActivityResponse> PostStoryActivityAsync(CreateStoryActivityRequest request)
        {
            const string endpoint = $"{BaseUrl}/activity-drafts/stories";
            ValidateRequest(request);
            return await PostJsonAsync<CreateStoryActivityRequest, CreateContentCreationActivityResponse>(endpoint, request);
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <exception cref="ValidationException"></exception>
        private void ValidateRequest(object request)
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
        private async Task<TResponse> PostJsonAsync<TRequest, TResponse>(string endpoint, TRequest requestPayload)
        {
            var jsonRequest = JsonSerializer.Serialize(requestPayload, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResponse>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"API Error: {response.StatusCode} - {errorContent}");

                if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests) // 429
                {
                    throw new HttpRequestException($"Rate limit exceeded. Please wait before retrying. Details: {errorContent}", null, response.StatusCode);
                }

                response.EnsureSuccessStatusCode();
                return default(TResponse); // Should not reach here
            }
        }
    }
}