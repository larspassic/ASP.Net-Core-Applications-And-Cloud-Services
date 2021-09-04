using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models
{
    public class UserEnrolledClassesViewModel
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public string UserName { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }

    }
}
