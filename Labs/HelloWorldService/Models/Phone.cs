using Newtonsoft.Json;

namespace HelloWorldService.Models
{
    public class Phone
    {
        [JsonProperty("Number", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Number { get; set; }
        
        [JsonProperty("Phone_Type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof (Newtonsoft.Json.Converters.StringEnumConverter))]
        public PhoneType PhoneType { get; set; }
    }
}