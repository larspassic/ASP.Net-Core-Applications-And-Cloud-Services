using Newtonsoft.Json;
using System;

namespace HelloWorldService.Models
{
    /// <summary>
    /// This is the contact object
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// This is the Id of the contact object.
        /// </summary>
        [JsonProperty("Id")]
        public int Id { get; set; }
        
        [JsonProperty("Name")]
        public string Name { get; set; }
        
        [JsonIgnore]
        public string Password { get; set; }

        [JsonProperty("Date_Added")]
        public DateTime DateAdded { get; set; }
        
        [JsonProperty("Phones")]
        public Phone[] Phones { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum PhoneType
        {
            Nil = 0,
            Home = 1,
            Mobile = 2,
        }
    }
}