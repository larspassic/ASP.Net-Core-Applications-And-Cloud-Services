using HelloWorldService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloWorldService.Controllers
{
    [Route("api/v2/contacts")]
    [ApiController]
    public class ContactsV2Controller : ControllerBase
    {
        // GET: api/<ContactsV2Controller>
        [HttpGet]
        //[Route("api/v2/contacts")]
        public IEnumerable<ContactV2> Get()
        {
            
            return new ContactV2[] { 
                new ContactV2 { Id=5, Name="steve-v2", Age=21 }
                };
        }

        // GET api/<ContactsV2Controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ContactsV2Controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ContactsV2Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContactsV2Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
