using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AddTwoNumbers
{
    public static class AddTwoNumbers
    {
        [FunctionName("AddTwoNumbers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            //string pnumber1 = req.Query["number1"];
            //string pnumber2 = req.Query["number2"];

            //int number1 = int.Parse(pnumber1);
            //int number2 = int.Parse(pnumber2);


            //The get version
            int number1 = data.number1;
            int number2 = data.number2;
            int sum = number1 + number2;



            string responseMessage = $"Hello. {number1} + {number2} = {sum}";

            return new OkObjectResult(responseMessage);
        }
    }
}
