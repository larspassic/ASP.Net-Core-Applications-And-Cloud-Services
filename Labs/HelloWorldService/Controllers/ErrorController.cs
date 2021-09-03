using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldService.Controllers
{
    
    
    /// <summary>
    /// This is my Error controller!
    /// </summary>
    /// <example>Here is an example.</example>
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        
        /// <summary>
        /// This is the primary Error route
        /// </summary>
        /// <param name="code">Http status code (Integer*32)</param>
        /// <returns></returns>
        [Route("/error/{code}")]
        [HttpGet]
        public IActionResult Error(int code) => new ObjectResult(new ApiResponse(code));

        [Route("/error2/{code}")]
        [HttpGet]
        public IActionResult Error2(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
