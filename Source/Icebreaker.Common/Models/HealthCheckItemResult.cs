using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Icebreaker.Common.Models
{
    public enum HealthStatus
    {
        Unhealthy,
        Healthy
    }

    public class HealthCheckItemResult
    {
        public string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HealthStatus HealthStatus { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorMessage { get; set; }
    }
}
