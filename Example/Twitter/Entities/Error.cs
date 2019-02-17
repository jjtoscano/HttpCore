using Newtonsoft.Json;
using System.Collections.Generic;

namespace Example.Twitter.Entities
{
    /// <summary>
    /// Errors list.
    /// </summary>
    public class ErrorsList
    {
        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>The errors.</value>
        [JsonProperty("errors")]
        public List<Error> Errors { get; set; }
    }

    /// <summary>
    /// Error.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [JsonProperty("code")]
        public long Code { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
