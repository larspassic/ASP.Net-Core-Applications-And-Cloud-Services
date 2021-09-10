using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_User_Service.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
