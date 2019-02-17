using Newtonsoft.Json;

namespace Example.Twitter.Entities
{
    /// <summary>
    /// OAuth token.
    /// </summary>
    public class OAuthToken
    {
        /// <summary>
        /// Token type (Bearer).
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// Token value.
        /// </summary>
        [JsonProperty("access_token")]
        public string Token { get; set; }
    }

}
