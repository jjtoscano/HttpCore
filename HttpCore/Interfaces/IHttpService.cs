using HttpCore.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace HttpCore.Interfaces
{
    /// <summary>
    /// HTTP Service Layer Interface.
    /// </summary>
    public interface IHttpService
    {
        /// <summary>
        /// Gets or sets the cookie container.
        /// </summary>
        CookieContainer CookieContainer { get; }

        /// <summary>
        /// Method to make HTTP requests. This method accepts parameters to configure the HTTP method, URL, ContentType, querystring parameters and headers.
        /// </summary>
        /// <typeparam name="T">Type of the custom error entity used to deserialize an error response from a HttpResponseMessage's content.</typeparam>
        /// <param name="method">HTTP methods accepted: GET,POST,PUT,DELETE. <see cref="CustomHttpMethod"/></param>
        /// <param name="url">The URL the request is sent to.</param>
        /// <param name="contentType">Request's ContentType.</param>
        /// <param name="parameters">Request's parameters.</param>
        /// <param name="headers">Request's header parameters.</param>
        /// <returns>Returns the string content of the HttpResponseMessage.</returns>
        Task<string> SendAsync<T>(CustomHttpMethod method, string url, string contentType = "application/x-www-form-urlencoded", List<KeyValuePair<string, string>> parameters = null, Dictionary<string, string> headers = null);

        /// <summary>
        /// Method to make HTTP requests to send objects as serialized string. This method accepts parameters to configure the HTTP method, URL, Entity object, ContentType and headers.
        /// </summary>
        /// <typeparam name="T">Type of the custom error entity used to deserialize an error response from a HttpResponseMessage's content.</typeparam>
        /// <param name="method">HTTP methods accepted: POST,PUT,PATCH. <see cref="CustomHttpMethod"/></param>
        /// <param name="url">The URL the request is sent to.</param>
        /// <param name="entity">Entity to serilize in JSON format.</param>
        /// <param name="contentType">Request's ContentType.</param>
        /// <param name="headers">Request's header parameters.</param>
        /// <returns>Returns the string content of the HttpResponseMessage.</returns>
        Task<string> SendObjectAsync<T>(CustomHttpMethod method, string url, Object entity, string contentType = "application/json", Dictionary<string, string> headers = null);

        /// <summary>
        /// Method to download a content as byte array.
        /// </summary>
        /// <typeparam name="T">Type of the custom error entity used to deserialize an error response from a HttpResponseMessage's content.</typeparam>
        /// <param name="url">The URL to download a content.</param>
        /// <returns>Returns a downloaded content as byte array.</returns>
        Task<byte[]> GetByteArrayAsync<T>(string url);
    }
}
