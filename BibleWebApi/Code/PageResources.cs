using System.Text.Json;
using System.Text.Json.Serialization;

namespace BibleWebApi.Code
{
    public static class PageResources
    {
        private static readonly JsonSerializerOptions m_jsonSerializerOptions;

        static PageResources()
        {
            m_jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            m_jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public static JsonSerializerOptions JsonSerializerOptions
        {
            get { return m_jsonSerializerOptions; }
        }
    }
}
