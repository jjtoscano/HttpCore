using Newtonsoft.Json;
using System.Collections.Generic;

namespace Example.Twitter.Entities
{
    /// <summary>
    /// User mention.
    /// </summary>
    public class UserMention
    {
        /// <summary>
        /// Screen name "@...".
        /// </summary>
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        /// <summary>
        /// Account name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Account ID.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }

        /// <summary>
        /// List of integers that contains start index and end index of the UserMention inside the tweet.
        /// </summary>
        [JsonProperty("indices")]
        public List<int> indices { get; set; }
    }
}
