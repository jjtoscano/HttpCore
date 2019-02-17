using Newtonsoft.Json;
using System.Collections.Generic;

namespace Example.Twitter.Entities
{
    /// <summary>
    /// Url.
    /// </summary>
    public class Url
    {
        /// <summary>
        /// Url.
        /// </summary>
        [JsonProperty("url")]
        public string URL { get; set; }

        /// <summary>
        /// Expanded Url.
        /// </summary>
        [JsonProperty("expanded_url")]
        public string ExpandedUrl { get; set; }

        /// <summary>
        /// Display Url.
        /// </summary>
        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        /// <summary>
        /// List of integers that contains start index and end index of the Url inside the tweet.
        /// </summary>
        [JsonProperty("indices")]
        public List<int> Indices { get; set; }
    }
}
