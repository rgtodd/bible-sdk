using System.Text.Json;
using System.Text.Json.Serialization;

namespace BibleCore.Utility
{
    public static class Serialization
    {
        private static readonly JsonSerializerOptions m_jsonSerializerOptions;

        static Serialization()
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
