using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ziggle.WebSite.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        
        //Here is where we compare the passwords during registration
        //If they don't match, this will probably create a model state isvalid false
        [Compare("Password")]
        
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
