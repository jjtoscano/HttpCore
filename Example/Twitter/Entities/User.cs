using Newtonsoft.Json;

namespace Example.Twitter.Entities
{
    /// <summary>
    /// User.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Account name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Screen name "@...".
        /// </summary>
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        /// <summary>
        /// Image Url.
        /// </summary>
        [JsonProperty("profile_background_image_url_https")]
        public string ProfileBackgroundURL { get; set; }
    }
}
