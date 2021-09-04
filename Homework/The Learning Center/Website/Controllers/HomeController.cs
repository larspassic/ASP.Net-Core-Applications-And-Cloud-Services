using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Website.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Website.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUserManager userManager;
        private readonly IClassManager classManager;
        private readonly IEnrollManager enrollManager;

        //The constructor
        public HomeController(IUserManager userManager, IClassManager classManager, IEnrollManager enrollManager)
        {

            this.userManager = userManager;

            this.classManager = classManager;

            this.enrollManager = enrollManager;
        }


        public IActionResult Index()
        {
            return View();
        }



        public ActionResult LogIn()
        {
            ViewData["ReturnUrl"] = Request.Query["returnUrl"];

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult EnrollInClass(int ClassId, string ClassName)
        {
            //Pull the UserID out of the Httpsession because the user won't send UserID in as a parameter
            var user = JsonConvert.DeserializeObject<Models.UserModel>(HttpContext.Session.GetString("User"));

            //This actually probably (maybe?) physically enrolls the user in the class,
            //and places the new row in the UserClass table in the database.
            var item = enrollManager.Add(user.Id, ClassId);

            var items = enrollManager.GetAll(user.Id)
                .Select(t => new Website.Models.EnrollModel
                {
                    UserId = t.UserId,
                    ClassId = t.ClassId
                }).ToArray();

            return View(items);


        }

        [Authorize]
        public ActionResult SelectClass(int id)
        {
            //Copied from the Class() action
            var classes = classManager.GetAllClasses().Select(t =>
                                    new Website.Models.ClassModel
                                    {
                                        ClassId = t.ClassId,
                                        ClassName = t.ClassName,
                                        ClassPrice = t.ClassPrice,
                                        ClassDescription = t.ClassDescription
                                    }).ToArray();

            var model = new ClassViewModel
            {
                Classes = classes
            };

            return View(new SelectList(classes, "ClassId", "ClassName"));
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    var json = JsonConvert.SerializeObject(new Website.Models.UserModel
                    {
                        Id = user.Id,
                        Name = user.Name
                    });

                    HttpContext.Session.SetString("User", json);

                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, "User"),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = false,
                        // Refreshing the authentication session should be allowed.

                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        IsPersistent = false,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        IssuedUtc = DateTimeOffset.UtcNow,
                        // The time at which the authentication ticket was issued.
                    };

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        authProperties).Wait();

                    return Redirect(returnUrl ?? "~/");
                }
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View(loginModel);
        }


        public ActionResult LogOff()
        {
            HttpContext.Session.Remove("User");

            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("~/");
        }




        [HttpGet]
        public ActionResult Register()
        {
            //We are not going to do this
            //ViewData["ReturnUrl"] = Request.Query["returnUrl"];

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult EnrolledClasses()
        {
            //Need to pull the user out of the current HTTP session
            var user = JsonConvert.DeserializeObject<Models.UserModel>(HttpContext.Session.GetString("User"));

            //Get the classes from the database
            var enrolledClasses = enrollManager.GetAll(user.Id)
                .Select(t => new Website.Models.EnrollModel
                {
                    UserId = t.UserId,
                    ClassId = t.ClassId
                }).ToArray();

            //Create a list of our custom object which holds userid, classid, and className
            List<UserEnrolledClassesViewModel> userEnrolledClassesViewModel = new List<UserEnrolledClassesViewModel>();

            foreach (var item in enrolledClasses)
            {
                //Create a new combined object which represents the complete picture of one class the user is registered for
                var singleClassObject = new UserEnrolledClassesViewModel();

                //Use the GetClassById method to pull in our ClassModel object with lots of details
                ClassModel classDetails = classManager.GetClassById(item.ClassId);

                //Trying to get UserName but it's not working
                //var userDetails = JsonConvert.DeserializeObject<Website.Models.UserModel>(Context.Session.GetString("User"));

                //Map all of the properties to the new object
                singleClassObject.UserId = item.UserId;
                singleClassObject.ClassId = item.ClassId;
                //singleClassObject.UserName = ??
                singleClassObject.ClassName = classDetails.ClassName;
                singleClassObject.ClassDescription = classDetails.ClassDescription;
                singleClassObject.ClassPrice = classDetails.ClassPrice;

                //Add the finished object to the list
                userEnrolledClassesViewModel.Add(singleClassObject);
            }

            
            //var classes = classManager.GetAllClasses().Select(t =>
            //                        new Website.Models.ClassModel
            //                        {
            //                            ClassId = t.ClassId,
            //                            ClassName = t.ClassName,
            //                            ClassPrice = t.ClassPrice,
            //                            ClassDescription = t.ClassDescription
            //                        }).ToArray();


            //List<string> listOfClassNames = new List<string>();

            //foreach (var singleClassId in enrolledClasses)
            //{
            //    var getClass = classManager.GetClassById(singleClassId.ClassId).ClassName;
            //    listOfClassNames.Add(user.Name + " " + singleClassId.ClassId + " " + getClass);
            //}




            //userEnrolledClassesViewModel.enrollModel = enrolledClasses[0];
            //userEnrolledClassesViewModel.enrollModel.UserId = enrolledClasses[0].UserId;
            //userEnrolledClassesViewModel.classModel;

            //for each enrolled class
            // { string classname = ClassManager.getClaassBybIDd(...)
            //userEnerolledClassesVieMmodel.classes."add classname to list"

            //}

            //Send the model object in to the page view
            return View(userEnrolledClassesViewModel);
        }



        [HttpPost]
        public ActionResult Register(RegisterModel registerModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                
                
                var user = userManager.Register(registerModel.UserName, registerModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    var json = JsonConvert.SerializeObject(new Website.Models.UserModel
                    {
                        Id = user.Id,
                        Name = user.Name
                    });

                    HttpContext.Session.SetString("User", json);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpGet]
        public ActionResult Class()
        {

            var classes = classManager.GetAllClasses().Select(t =>
                                    new Website.Models.ClassModel
                                    {
                                        ClassId = t.ClassId,
                                        ClassName = t.ClassName,
                                        ClassPrice = t.ClassPrice,
                                        ClassDescription = t.ClassDescription
                                    }).ToArray();

            var model = new ClassViewModel
            {
                Classes = classes
            };

            return View(model);
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
