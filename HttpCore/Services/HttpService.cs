using HttpCore.Entities;
using HttpCore.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HttpCore.Services
{
    /// <summary>
    /// HTTP Service Layer.
    /// </summary>
    public class HttpService : IHttpService, IDisposable
    {
        /// <summary>
        /// HttpClient.
        /// </summary>
        private HttpClient client;

        /// <summary>
        /// HttpClient without cookies.
        /// </summary>
        private HttpClient clientWithoutCookies;

        /// <summary>
        /// Network credential to authenticate a HttpClient through a proxy.
        /// </summary>
        private NetworkCredential networkCredential;

        /// <summary>
        /// Default constructor that initializes a new instance of HttpClient without proxy.
        /// </summary>
        public HttpService()
        {
            #if DEBUG

            ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
            {
                return true;
            };

            #endif

            this.client = this.GenerateHttpClient();
            this.clientWithoutCookies = this.GenerateHttpClientWithoutCookies();
        }

        /// <summary>
        /// Initializes a new instance of HttpClient with proxy.
        /// </summary>
        /// <param name="proxyUrl">Proxy URL.</param>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <param name="domain">User domain.</param>
        public HttpService(string proxyUrl, string userName, string password, string domain = "")
        {
            #if DEBUG

            ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
            {
                return true;
            };

            #endif

            this.networkCredential = domain == string.Empty ? new NetworkCredential(userName, password) : new NetworkCredential(userName, password, domain);
            this.client = this.GenerateHttpClientProxy(proxyUrl);
            this.clientWithoutCookies = this.GenerateHttpClientProxyWithoutCookies(proxyUrl);
        }

        /// <summary>
        /// Gets or sets the cookie container.
        /// </summary>
        public CookieContainer CookieContainer { get; set; }

        /// <summary>
        /// Initializes a new instance of HttpClient without cookie container.
        /// </summary>
        /// <returns>Returns a new instance of HttpClient without cookie container.</returns>
        private HttpClient GenerateHttpClientWithoutCookies()
        {
            return new HttpClient(new HttpClientHandler());
        }

        /// <summary>
        /// Generates the http client proxy without cookies.
        /// </summary>
        /// <returns>The http client proxy without cookies.</returns>
        /// <param name="urlProxy">URL proxy.</param>
        private HttpClient GenerateHttpClientProxyWithoutCookies(string urlProxy)
        {
            return new HttpClient(new HttpClientHandler()
            {
                Proxy = new WebProxy(new Uri(urlProxy), false)
                {
                    UseDefaultCredentials = false,
                    Credentials = networkCredential
                },
                UseDefaultCredentials = false,
                Credentials = networkCredential
            });
        }

        /// <summary>
        /// Generates the http client.
        /// </summary>
        /// <returns>The http client.</returns>
        private HttpClient GenerateHttpClient()
        {
            this.CookieContainer = new CookieContainer();

            return new HttpClient(new HttpClientHandler
            {
                CookieContainer = this.CookieContainer
            });
        }

        /// <summary>
        /// Generates the http client proxy.
        /// </summary>
        /// <returns>The http client proxy.</returns>
        /// <param name="urlProxy">URL proxy.</param>
        private HttpClient GenerateHttpClientProxy(string urlProxy)
        {
            this.CookieContainer = new CookieContainer();

            return new HttpClient(new HttpClientHandler()
            {
                Proxy = new WebProxy(new Uri(urlProxy), false)
                {
                    UseDefaultCredentials = false,
                    Credentials = networkCredential
                },
                UseDefaultCredentials = false,
                CookieContainer = this.CookieContainer,
                Credentials = networkCredential
            });
        }

        /// <summary>
        /// Method that throws an exception <see cref="HttpServiceException{T}"/> when the <paramref name="textResponse"/> can be deserialized into T object.
        /// </summary>
        /// <typeparam name="T">Type of the custom error entity used to deserialize an error response from a HttpResponseMessage's content.</typeparam>
        /// <param name="statusCode">The HTTP status code returned in the response.</param>
        /// <param name="textResponse">The string content of the HttpResponseMessage.</param>
        private void ShouldThrowException<T>(HttpStatusCode statusCode, string textResponse)
        {
            try
            {
                // if the text response can be be deserialize into T, we will know there is a API error
                T error = JsonConvert.DeserializeObject<T>(textResponse);
                throw new HttpServiceException<T>((int)statusCode, error);
            }
            catch (JsonException) { }
        }

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
        public async Task<string> SendAsync<T>(CustomHttpMethod method, string url, string contentType = "application/x-www-form-urlencoded", List<KeyValuePair<string, string>> parameters = null, Dictionary<string, string> headers = null)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                string data = string.Empty;

                if (parameters != null)
                {
                    List<string> pairs = new List<string>();
                    parameters.ForEach(parameter => pairs.Add($"{parameter.Key}={WebUtility.UrlEncode(parameter.Value)}"));
                    data = string.Join("&", pairs);
                }

                var httpContent = new StringContent(data, Encoding.UTF8, contentType);

                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> keyValuePair in headers)
                    {
                        client.DefaultRequestHeaders.Remove(keyValuePair.Key);
                        client.DefaultRequestHeaders.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                }

                HttpResponseMessage response = null;
                switch (method)
                {
                    case CustomHttpMethod.Get:
                        response = await this.client.GetAsync(url);
                        break;
                    case CustomHttpMethod.Post:
                        response = await this.client.PostAsync(url, httpContent);
                        break;
                    case CustomHttpMethod.Put:
                        response = await this.client.PutAsync(url, httpContent);
                        break;
                    case CustomHttpMethod.Delete:
                        response = await this.client.DeleteAsync(url);
                        break;
                }

                var responseText = await response.Content.ReadAsStringAsync();
                this.ShouldThrowException<T>(response.StatusCode, responseText);

                return responseText;
            }
            catch (HttpRequestException)
            {
                return null;
            }
            finally
            {
                client.DefaultRequestHeaders.Accept.Clear();
            }
        }

        /// <summary>
        /// Method to make HTTP requests to send objects as serialized string. This method accepts parameters to configure the HTTP method, URL, Entity object, ContentType and headers.
        /// </summary>
        /// <typeparam name="T">Type of the custom error entity used to deserialize an error response from a HttpResponseMessage's content.</typeparam>
        /// <param name="method">HTTP methods accepted: POST,PUT,PATCH. <see cref="CustomHttpMethod"/></param>
        /// <param name="url">The URL the request is sent to.</param>
        /// <param name="entity">Entity to serilize in JSON format.</param>
        /// <param name="contentType">Request's ContentType..</param>
        /// <param name="headers">Request's header parameters.</param>
        /// <returns>Returns the string content of the HttpResponseMessage.</returns>
        public async Task<string> SendObjectAsync<T>(CustomHttpMethod method, string url, Object entity, string contentType = "application/json", Dictionary<string, string> headers = null)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                var jsonString = JsonConvert.SerializeObject(entity);
                var httpContent = new StringContent(jsonString, Encoding.UTF8, contentType);

                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> keyValuePair in headers)
                    {
                        client.DefaultRequestHeaders.Remove(keyValuePair.Key);
                        client.DefaultRequestHeaders.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                }

                HttpResponseMessage response = null;
                switch (method)
                {
                    case CustomHttpMethod.Post:
                        response = await this.client.PostAsync(url, httpContent);
                        break;
                    case CustomHttpMethod.Put:
                        response = await this.client.PutAsync(url, httpContent);
                        break;
                    case CustomHttpMethod.Patch:
                        var request = new HttpRequestMessage(new HttpMethod("PATCH"), url) { Content = httpContent };
                        response = await this.client.SendAsync(request);
                        break;
                }

                var responseText = await response.Content.ReadAsStringAsync();
                this.ShouldThrowException<T>(response.StatusCode, responseText);

                return responseText;
            }
            catch (HttpRequestException)
            {
                return null;
            }
            finally
            {
                client.DefaultRequestHeaders.Accept.Clear();
            }
        }

        /// <summary>
        /// Method to download a content as byte array.
        /// </summary>
        /// <typeparam name="T">Type of the custom error entity used to deserialize an error response from a HttpResponseMessage's content.</typeparam>
        /// <param name="url">The URL to download a content.</param>
        /// <returns>Returns a downloaded content as byte array.</returns>
        public async Task<byte[]> GetByteArrayAsync<T>(string url)
        {
            byte[] responseBytes;

            try
            {
                HttpResponseMessage response = await this.client.GetAsync(url);

                responseBytes = await response.Content.ReadAsByteArrayAsync();

                this.ShouldThrowException<T>(response.StatusCode, Encoding.UTF8.GetString(responseBytes));
            }
            catch (HttpRequestException)
            {
                return null;
            }

            return responseBytes;
        }

        /// <summary>
        /// Releases the unmanaged resources and disposes of the managed resources used by the invoker.
        /// </summary>
        public void Dispose()
        {
            this.client.Dispose();
        }
    }
}
