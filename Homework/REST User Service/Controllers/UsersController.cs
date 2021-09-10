using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using REST_User_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace REST_User_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //"Database" of users
        public static List<User> users = new List<User>();
        
        
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            
            
            return users;
        }

        // GET api/<UsersController>/5
        [HttpGet("{guid}", Name = "Get")]
        public IActionResult Get(Guid guid)
        {
            //Search through the user database and store the result that was sent in
            User userFound = users.FirstOrDefault(t => t.Id == guid);
            
            //Check if user was not found and send back a 404 result instead
            if (userFound == null)
            {
                
                //Specifying the guid so it will show the user what they sent over
                return NotFound("User not found: " + guid);
            }
            
            return Ok(userFound);
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {
            if (value == null)
            {
                return new BadRequestResult();
            }
            if (value.Email == null)
            {
                return BadRequest("Email field can not be null.");
            }
            if (value.Password == null)
            {
                return BadRequest("Password field can not be null.");
            }

            //Set this user's value to be a new GUID
            value.Id = System.Guid.NewGuid();
            value.CreatedDate = DateTime.UtcNow;

            //Actually add the user to the database
            users.Add(value);

            return CreatedAtAction(nameof(Get), new { id = value.Id });
        }

        // PUT api/<UsersController>/5
        [HttpPut("{guid}")]
        public IActionResult Put(Guid guid, [FromBody] User value)
        {
            //Search through the user database and store the result that was sent in
            User userFound = users.FirstOrDefault(t => t.Id == guid);

            //If the user was found, use the input to update the user in the database
            if (userFound != null)
            {

                //Check each field, only update the field if something was included in the request
                if (value.Email != null)
                {
                    userFound.Email = value.Email;
                }
                if (value.Password != null)
                {
                    userFound.Password = value.Password;
                }
                if (value.Note != null)
                {
                    userFound.Note = value.Note;
                }

                return Ok(userFound.Id);
            }
            else return new NotFoundObjectResult(guid);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            //Search through the user database and store the result that was sent in
            User userFound = users.FirstOrDefault(t => t.Id == guid);

            if (userFound != null)
            {
                users.Remove(userFound);

                return new OkResult();
            }
            else
            {
                return new NotFoundObjectResult("Contact not found. No records were deleted.");
            }

        }
    }
}
