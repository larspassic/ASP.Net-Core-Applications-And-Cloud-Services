using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace HelloWorldClient
{
    public class Contact
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("date_added")]
        public DateTime DateAdded { get; set; }

        [JsonProperty("phones")]
        public Phone[] Phones { get; set; }
    }

    public class Phone
    {
        [JsonProperty("phone_number")]
        public string Number { get; set; }

        [JsonProperty("phone_type")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public PhoneType PhoneType { get; set; }
    }

    public enum PhoneType
    {
        Home,
        Mobile,
    }

    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();

            //"Do not forget that trailing slash!"
            client.BaseAddress = new Uri("http://localhost:43953/api/");

            var newContact = new Contact
            {
                Name = "New Name",
                Phones = new[] {new Phone {Number = "425-111-2222",PhoneType = PhoneType.Mobile}}
            };

            var newJson = JsonConvert.SerializeObject(newContact);
            var postContent = new StringContent(newJson, System.Text.Encoding.UTF8, "application/json");
            var postResult = client.PostAsync("contacts", postContent).Result;
            Console.WriteLine(postResult.StatusCode);


            var result = client.GetAsync("contacts").Result;
            var json = result.Content.ReadAsStringAsync().Result;
            var list = JsonConvert.DeserializeObject<List<Contact>>(json);
            
            //Exercise to add delete functionality - use .Result to get the result NOW
            var idToDelete = list[0].Id;
            var deleteResult = client.DeleteAsync("contacts/"+ idToDelete);
            //var deletejson = deleteResult

            Console.WriteLine($"{deleteResult.Result}");

            foreach (var contact in list)
            {
                Console.WriteLine("Name: {0}", contact.Name);
            }

            Console.WriteLine(json);
            Console.ReadLine();
        }
    }
}
