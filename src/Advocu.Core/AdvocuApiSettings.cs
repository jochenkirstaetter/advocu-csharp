namespace Advocu
{
    /// <summary>
    /// Configuration settings for the Advocu API client.
    /// </summary>
    public sealed class AdvocuApiSettings
    {
        /// <summary>
        /// Gets or sets the base URL of the Advocu API.
        /// </summary>
        public string? BaseUrl { get; set; }
        /// <summary>
        /// Gets or sets the access token for authentication.
        /// </summary>
        public string? AccessToken { get; set; }
    }
}