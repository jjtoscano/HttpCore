using Example.Twitter.Entities;
using HttpCore.Services;
using HttpCore.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example.Twitter
{
    public static class TwitterService
    {
        public static HttpService httpService = new HttpService();

        public static async Task<OAuthToken> OAuth(string consumerKey, string secretKey)
        {
            OAuthToken oAuthToken = null;

            string url = "https://api.twitter.com/oauth2/token";

            if (!string.IsNullOrEmpty(url))
            {
                string authorization = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{consumerKey}:{secretKey}"));

                List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();

                param.Add(new KeyValuePair<string, string>("Content-Type", "application/x-www-form-urlencoded"));
                param.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", string.Format("Basic {0}", authorization));
                headers.Add("grant_type", "client_credentials");

                var response = await httpService.SendAsync<ErrorsList>(CustomHttpMethod.Post, url, "application/x-www-form-urlencoded", param, headers);

                if (!string.IsNullOrEmpty(response))
                {
                    oAuthToken = JsonConvert.DeserializeObject<OAuthToken>(response);
                }
            }
           

            return oAuthToken;
        }

    }
}
