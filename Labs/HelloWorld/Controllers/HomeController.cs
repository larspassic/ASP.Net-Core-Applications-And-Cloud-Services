using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelloWorld.Models;


namespace HelloWorld.Controllers
{
    
    [LoggingAttribute]
    public class HomeController : Controller
    {
        //This is a field
        private IProductRepository productRepository;

        //This is a constructor
        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public PartialViewResult IncrementCount()
        {
            

            return new PartialViewResult();
        }
        
        
        // GET: Home
        public ActionResult Index()
        {
            //Divide by zero error to demonstrate exceptions
            //int x = 1;
            //x = x / (x - 1);
            
            
            
            return View();
        }


        //Login action for exercise
        [HttpGet]
        public ActionResult Login()
        {


            return View();
        }

        [HttpPost]
        //Trying to make an HttpPost version of the Login action
        public ActionResult Login(Models.LoginModel loginModel)
        {
            HttpContext.Session.Add("UserName", loginModel.UserName);
            return View("Index", loginModel);
        }

        //Logoff action for exercise
        public ActionResult Logoff()
        {
            Session["UserName"] = null;

            return RedirectToAction("Index");
        }

        //DisplayLoginName for exercise
        public PartialViewResult DisplayLoginName()
        {
            return new PartialViewResult();
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

            return View(productRepository.Products.First());
        }

        public ActionResult Products()
        {

            return View(productRepository.Products);
        }
    }
}