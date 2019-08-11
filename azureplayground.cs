using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace docdemo
{
    public static class azureplayground
    {
        [FunctionName("azureplayground")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string value = req.Query["value"];
            string from = req.Query["from"];
            string to = req.Query["to"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            value = value ?? data?.value;
            from = from ?? data?.from;
            to = to ?? data?.to;

            if (value == null || from == null || to == null) {
                return new BadRequestObjectResult("Please pass all mandatory values");
            }

            Unit unit = UnitFactory.getUnitObject(from, to, value);

            if (unit == null) {
                return new BadRequestObjectResult("Invalid units");
            }

            Nullable<double> resultValue = unit.convert(from, to);
            if (resultValue == null) {
                return new BadRequestObjectResult("Invalid conversion");
            }

            return (ActionResult)new OkObjectResult($"Convertion from {from} to {to} \nResult: {resultValue}");
        }
    }
}
