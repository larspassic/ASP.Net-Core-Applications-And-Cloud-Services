using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelloWorld.Models;

namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Divide by zero error to demonstrate exceptions
            //int x = 1;
            //x = x / (x - 1);
            
            
            
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        ////Exception only for this controller
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    base.OnException(filterContext);
        //}

        [HttpGet]
        public ActionResult RsvpForm()
        {
            var guestResponse = new Models.GuestResponse
            {
                //Name = "Dave",
                //Email = "Dave@Rawlinson.net",
                //Phone = "555-7600",
                
                SelectItems = new[]
                {
                    new SelectListItem { Text = "Yes, I'll be there", Value = bool.TrueString },
                    new SelectListItem { Text = "No, I can't come", Value = bool.FalseString },
                }
            };
            
            return View(guestResponse);
        }

        [HttpPost]
        public ActionResult RsvpForm(Models.GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                return View("Thanks", guestResponse);
            }
            else
            {
                guestResponse.SelectItems = new[]
                {
                    new SelectListItem { Text = "Yes, I'll be there", Value = bool.TrueString },
                    new SelectListItem { Text = "No, I can't come", Value = bool.FalseString },
                };
                
                return View(guestResponse);
            }
            
        }

        public ActionResult Product()
        {
            var myProduct = new Product
            {
                ProductId = 1,
                Name = "Kayak",
                Description = "A boat for one person",
                Category = "water-sports",
                Price = 200m,
            };

            return View(myProduct);
        }

        public ActionResult Products()
        {
            var products = new Product[]
            {
                new Product{ ProductId = 1, Name = "Tootsie Roll", Price = 0.10m},
                new Product{ ProductId = 2, Name = "Hard candy", Price = 0.25m},
                new Product{ ProductId = 3, Name = "Lollipop", Price = 0.50m},
                new Product{ ProductId = 4, Name = "Gummy Bears", Price = 1.25m}
            };

            return View(products);
        }
    }
}