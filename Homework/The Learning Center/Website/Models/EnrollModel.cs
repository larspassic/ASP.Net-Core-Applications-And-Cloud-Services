using Microsoft.AspNetCore.Mvc.Rendering; //Added to support the <SelectListItem> objects
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models
{
    public class EnrollModel
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }

        //Adding this to try to get the enroll page to work
        //public List<SelectListItem> SelectListItemsOfClassNames { get; set; } //didn't end up using this


        //I think this is a constructor that creates an empty list of "Select List Items"
        //public EnrollModel() //Didn't end up using this
        //{
        //    SelectListItemsOfClassNames = new List<SelectListItem>();
        //}
    }
}
