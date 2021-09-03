using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;

namespace HelloWorldService
{
    public class LoggingActionFilter : IActionFilter
    {
        private System.Diagnostics.Stopwatch stopwatch;

        private IHostingEnvironment env;

        public LoggingActionFilter(IHostingEnvironment env)
        {
            this.env = env;
        }

        //Called before the action
        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
        }


        //Called after the action
        public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            stopwatch.Stop();


            //Form the pieces that will be used during logging
            var webroot = env.WebRootPath;
            var controller = (ControllerBase)actionExecutedContext.Controller;
            var controllerName = controller.ToString();
            var actionName = controller.Request.Method;
            var filepath = Path.Combine(webroot, "logger.txt");
            
            //Build the log line, piece by piece
            var logline = string.Format(
                $"{System.DateTime.Now} : " +
                $"Controller={controllerName}, " +
                $"Action={actionName}, " +
                $"Elapsed={stopwatch.Elapsed}\n");

            File.AppendAllText(filepath, logline);
        }
    }
}