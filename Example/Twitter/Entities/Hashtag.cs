using Newtonsoft.Json;
using System.Collections.Generic;

namespace Example.Twitter.Entities
{
    /// <summary>
    /// Hashtag.
    /// </summary>
    public class Hashtag
    {
        /// <summary>
        /// Hashtag text.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// List of integers that contains start index and end index of the hashtag inside the tweet.
        /// </summary>
        [JsonProperty("indices")]
        public List<int> Indices { get; set; }
    }
}
