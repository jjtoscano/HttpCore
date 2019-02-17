using System;
using System.Net;

namespace HttpCore.Entities
{
    /// <summary>
    /// Exception class for the HTTP service layer.
    /// </summary>
    /// <typeparam name="T">Type of the custom error entity used to deserialize an error response from a HttpResponseMessage's content.</typeparam>
    public class HttpServiceException<T> : Exception
    {
        /// <summary>
        /// Initializes a new instance of the HttpServiceException<typeparamref name="T"/> with a specific status code and response.  
        /// </summary>
        /// <param name="responseStatusCode">HTTP status code returned into HttpResponseMessage. <see cref="HttpStatusCode"/></param>
        /// <param name="errorResponse">Deserialized error response returned into HttpResponseMessage.</param>
        public HttpServiceException(int responseStatusCode, T errorResponse)
        {
            this.ResponseStatusCode = responseStatusCode;
            this.Error = errorResponse;
        }

        /// <summary>
        /// Gets or sets the desiarilized response error.
        /// </summary>
        public T Error { get; private set; }

        /// <summary>Gets or sets the HTTP status code returned in the response.</summary>
        public int ResponseStatusCode { get; private set; }
    }
}
