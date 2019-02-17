using Newtonsoft.Json;
using System.Collections.Generic;

namespace Example.Twitter.Entities
{
    /// <summary>
    /// Entities.
    /// </summary>
    public class Entities
    {
        /// <summary>
        /// List of hashtags(#). <see cref="Hashtag"/>
        /// </summary>
        [JsonProperty("hashtags")]
        public List<Hashtag> Hashtags { get; set; }

        /// <summary>
        /// List of symbols.
        /// </summary>
        [JsonProperty("symbols")]
        public List<object> Symbols { get; set; }

        /// <summary>
        /// List of user's mentions(@). <see cref="UserMention"/>
        /// </summary>
        [JsonProperty("user_mentions")]
        public List<UserMention> UserMentions { get; set; }

        /// <summary>
        /// List of URLs. <see cref="Url"/>
        /// </summary>
        [JsonProperty("urls")]
        public List<Url> Urls { get; set; }

        /// <summary>
        /// List of media URLs. <see cref="Url"/>
        /// </summary>
        [JsonProperty("media")]
        public List<Url> MediaUrls { get; set; }
    }
}
