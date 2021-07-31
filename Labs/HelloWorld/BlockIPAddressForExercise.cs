using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace HelloWorld
{
    public class BlockIPAddressForExercise : ActionFilterAttribute
    {
        //Called by the ASP.NET MVC framework BEFORE the action method executes.
        //public override void 
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentRequest = filterContext.HttpContext.Request;

            if (currentRequest.UserHostAddress == "::1")
            {
                filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);


                //I added this. I think it was breaking the connection too much.
                //currentRequest.Abort();
            }
            
            base.OnActionExecuting(filterContext);
        }


        
    }
}