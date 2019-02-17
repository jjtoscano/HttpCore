using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Example.Twitter.Entities
{
    /// <summary>
    /// Twitter date time converter.
    /// </summary>
	public class TwitterDateTimeConverter : DateTimeConverterBase
	{
        /// <summary>
        /// Converts from twitter format.
        /// </summary>
        /// <returns>The datetime in twitter format.</returns>
        /// <param name="utcTime">UTC time.</param>
        internal DateTime? ConvertFromTwitterFormat(object utcTime)
		{
            DateTime? result;

            if (utcTime is DateTime)
            {
                result = ((DateTime)utcTime).ToUniversalTime();
            }
            else if (utcTime is string)
            {
                result = DateTime.ParseExact(utcTime.ToString(), "ddd MMM dd HH:mm:ss K yyyy", System.Globalization.CultureInfo.InvariantCulture).ToUniversalTime();
            }
            else
            {
                throw new Exception("Expected date object value.");
            }

            return result;
		}

        /// <summary>
        /// Reads the json.
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="reader">Reader tha contains the readed value.</param>
        /// <param name="objectType">Object type.</param>
        /// <param name="existingValue">Existing value.</param>
        /// <param name="serializer">Serializer.</param>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			object result = null;

			result = ConvertFromTwitterFormat(reader.Value);

			return result;
		}

        /// <summary>
        /// Writes the json.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="value">Value.</param>
        /// <param name="serializer">Serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //if there is need to write, you will have to implement this method.
            throw new NotImplementedException();
        }
    }
}