using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace Example.Twitter.Entities
{
    /// <summary>
    /// Tweet.
    /// </summary>
    public class Tweet
    {
        /// <summary>
        /// Full text without format.
        /// </summary>
        [JsonProperty("full_text")]
        public string FullText { get; set; }

        /// <summary>
        /// Text without format.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Created datetime.
        /// </summary>
        [JsonConverter(typeof(TwitterDateTimeConverter))]
        [JsonProperty("created_at")]
        public DateTime Created { get; set; }

        /// <summary>
        /// User profile.
        /// </summary>
        [JsonProperty("user")]
        public User User { get; set; }

        /// <summary>
        /// Entidades del tweet: Hashtags y menciones a otros usuarios.
        /// </summary>
        [JsonProperty("entities")]
        public Entities Entities { get; set; }

        /// <summary>
        /// Constructor JSON para inicializar las propiedad calculada "HtmlText".
        /// </summary>
        /// <param name="FullText">Texto completo del tweet sin formato.</param>
        /// <param name="Text">Texto del tweet sin formato.</param>
        /// <param name="Entities">Entidades del tweet: Hashtags y menciones a otros usuarios.</param>
        [JsonConstructor]
        public Tweet(string FullText, string Text, Entities Entities)
        {
            if (!string.IsNullOrEmpty(FullText))
            {
                HtmlText = FullText;
            }
            else if (!string.IsNullOrEmpty(Text))
            {
                HtmlText = Text;
            }

            if (!string.IsNullOrEmpty(HtmlText))
            {
                if(Entities.Hashtags != null)
                {
                    foreach (Hashtag hashtag in Entities.Hashtags)
                    {
                        HtmlText = Regex.Replace(HtmlText, string.Format("(?<hashtag>#{0}\\b)", hashtag.Text), string.Format("<a href='https://twitter.com/hashtag/{0}?src=hash'>#{1}</a>", hashtag.Text, hashtag.Text));
                    }
                }

                if (Entities.UserMentions != null)
                {
                    foreach (UserMention mention in Entities.UserMentions)
                    {
                        HtmlText = Regex.Replace(HtmlText, string.Format("(?<mention>@{0}\\b)", mention.ScreenName), string.Format("<a href='https://twitter.com/{0}'>@{1}</a>", mention.ScreenName, mention.ScreenName));
                    }
                }
                
                if (Entities.Urls != null)
                {
                    foreach (Url url in Entities.Urls)
                    {
                        HtmlText = Regex.Replace(HtmlText, string.Format("(?<url>{0}\\b)", Regex.Escape(url.URL)), string.Format("<a href='{0}'>{1}</a>", url.ExpandedUrl, url.DisplayUrl));
                    }
                }

                if (Entities.MediaUrls != null)
                {
                    foreach (Url mediaUrl in Entities.MediaUrls)
                    {
                        HtmlText = Regex.Replace(HtmlText, string.Format("(?<url>{0}\\b)", Regex.Escape(mediaUrl.URL)), string.Format("<a href='{0}'>{1}</a>", mediaUrl.ExpandedUrl, mediaUrl.DisplayUrl));
                    }
                }
            }
        }

        /// <summary>
        /// Texto del tweet con formato HTML.
        /// </summary>
        public string HtmlText { get; set; }
    }
}
