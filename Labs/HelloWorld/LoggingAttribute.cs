using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace HelloWorld
{
    public class LoggingAttribute : ActionFilterAttribute
    {
        private Stopwatch stopwatch;

        //Called by the ASP.NET MVC framework BEFORE the action method executes.
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentRequest = filterContext.HttpContext.Request;

            stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            base.OnActionExecuting(filterContext);
        }


        //Called by the ASP.NET MVC framework AFTER the action method executes.
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var currentResponse = filterContext.HttpContext.Response;

            stopwatch.Stop();

            var milliseconds = stopwatch.ElapsedMilliseconds;

            System.IO.File.AppendAllText(System.Web.HttpContext.Current.Server.MapPath("~/Logger.txt"), string.Format($"{System.DateTime.Now} : Elapsed={stopwatch.Elapsed} : Action={filterContext.ActionDescriptor.ActionName}\n"));
            
            base.OnActionExecuted(filterContext);
        }
    }
}