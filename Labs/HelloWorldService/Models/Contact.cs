using Newtonsoft.Json;
using System;

namespace HelloWorldService.Models
{
    public class Contact
    {
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
    }
}