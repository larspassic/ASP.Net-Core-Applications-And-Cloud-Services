using HelloWorldService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloWorldService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authenticator] //this will complicate Javascript section

    public class ContactsController : ControllerBase
    {

        private static List<Contact> contacts = new List<Contact>();

        private static int currentId = 101;
        
        // GET: api/<ContactsController>
        [HttpGet]
        public IActionResult Get()
        {

            //int x = 1;
            //x = x / (x - 1);
            
            return Ok(contacts);
        }

        // GET api/<ContactsController>/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            //Search through "contacts" and store the result where id matches the id that was sent
            Contact contactFound = contacts.FirstOrDefault(t => t.Id == id);


            //Exercise - need to send back a 404 not found result, how if we're sending back a Contact object
            if (contactFound == null)
            {
                return NotFound();
            }

            return Ok(contactFound);
        }

        // POST api/<ContactsController>
        [HttpPost]
        public IActionResult Post([FromBody] Contact value)
        {
            if (value == null)
            {
                return new BadRequestResult();
            }
            if (value.Name == null)
            {
                return BadRequest(new ErrorResponse { Message = "Name field is null" });
            }

            //Set this contact's value to be the currentId and increment it
            value.Id = currentId++;
            value.DateAdded = DateTime.Now;
            contacts.Add(value);

            //var result = new { Id = value.Id, Candy = true };
            //var result = value;

            //Look at the "Location" header in the response output in Postman to see what this is sending back after the POST
            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);

        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Contact value)
        {
            //Try to find the contact
            Contact contactFound = contacts.FirstOrDefault(t => t.Id == id);

            //Easy but not good way to do it, because it overwrites things like 
            //contactFound = value;


            if (contactFound != null)
            {
                //Make the changes that were sent in via the "value" param
                contactFound.Name = value.Name;
                contactFound.Phones = value.Phones;

                return Ok(contactFound);
            }
            else
            {
                return new NotFoundResult();
            }

        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //Try to find the contact that matches the Id
            Contact contactFound = contacts.FirstOrDefault(t => t.Id == id);
            
            //If we found a contact
            if (contactFound != null)
            {
                //Actually remove the contact
                contacts.Remove(contactFound);

                return new OkResult();
            }
            else
            {
                //"contact not found. No records were deleted."

                return new NotFoundResult();
            }

            //Alternate method
            //var contactsDeleted = contacts.RemoveAll(t => t.Id == id);

            //if (contactsDeleted == 0)
            //{
            //    return NotFound();
            //}
        }
    }
}
