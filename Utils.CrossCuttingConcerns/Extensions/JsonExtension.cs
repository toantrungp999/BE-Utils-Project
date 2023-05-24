using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Utils.CrossCuttingConcerns.Extensions
{
    public static class JsonExtension
	{
		/// <summary>
		/// Parse an json to an object or default.
		/// </summary>
		public static T? ToObject<T>(this string json)
		{
			var test = json.IsValidJson()
                ? JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings())
                : default;
            return json.IsValidJson()
				? JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings())
				: default;
		}

		/// <summary>
		/// Try parse and output a result. Otherwise, return false.
		/// </summary>
		public static bool TryParse<T>(this string json, out T? result)
		{
			result = default;

			if (!json.IsValidJson())
			{
				return false;
			}

			try
			{
				result = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings());

				return true;
			}
			catch
			{
				// Trying to parse the result into an object.
				// If any errors occur that means it cannot be parsed, then return false.
			}

			return false;
		}

		/// <summary>
		/// Check if a string is a valid json format.
		/// </summary>
		public static bool IsValidJson(this string json)
		{
			json = json.Trim();

			if (json.StartsWith("{") && json.EndsWith("}") ||
			    json.StartsWith("[") && json.EndsWith("]"))
			{
				try
				{
					JToken.Parse(json);
					return true;
				}
				catch (JsonReaderException)
				{
					return false;
				}
				catch (Exception)
				{
					return false;
				}
			}

			return false;
		}
	}
}
