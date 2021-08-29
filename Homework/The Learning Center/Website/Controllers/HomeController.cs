﻿using Microsoft.AspNetCore.Mvc;
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

namespace Website.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUserManager userManager;
        private readonly IClassManager classManager;

        //The constructor
        public HomeController(IUserManager userManager, IClassManager classManager)
        {

            this.userManager = userManager;

            this.classManager = classManager;
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
                

                    //Commenting this section out because we think it is 
                //    var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, user.Name),
                //    new Claim(ClaimTypes.Role, "User"),
                //};

                //    var claimsIdentity = new ClaimsIdentity(claims,
                //        CookieAuthenticationDefaults.AuthenticationScheme);

                //    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                //    var authProperties = new AuthenticationProperties
                //    {
                //        AllowRefresh = false,
                //        // Refreshing the authentication session should be allowed.

                //        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                //        // The time at which the authentication ticket expires. A 
                //        // value set here overrides the ExpireTimeSpan option of 
                //        // CookieAuthenticationOptions set with AddCookie.

                //        IsPersistent = false,
                //        // Whether the authentication session is persisted across 
                //        // multiple requests. When used with cookies, controls
                //        // whether the cookie's lifetime is absolute (matching the
                //        // lifetime of the authentication ticket) or session-based.

                //        IssuedUtc = DateTimeOffset.UtcNow,
                //        // The time at which the authentication ticket was issued.
                //    };

                //    HttpContext.SignInAsync(
                //        CookieAuthenticationDefaults.AuthenticationScheme,
                //        claimsPrincipal,
                //        authProperties).Wait();

                    return Redirect(returnUrl ?? "~/");
                }
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpGet]
        public ActionResult Class()
        {

            var classes = classManager
                                .GetAllClasses()
                                .Select(t =>
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
