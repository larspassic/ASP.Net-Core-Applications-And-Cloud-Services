using Newtonsoft.Json;

namespace HelloWorldService.Tests
{
    public class Phone
    {
        [JsonProperty("phone_number")]
        public string Number { get; set; }

        [JsonProperty("phone_type")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public PhoneType PhoneType { get; set; }
    }
}