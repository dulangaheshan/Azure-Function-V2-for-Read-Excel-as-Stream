
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using IronXL;
using ExcelDataReader;
using System.Collections.Generic;
using System.Data;
using System;

namespace email_to_db
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            //dynamic formdata = req.ReadFormAsync();
            try
            {
                dynamic file = req.Form.Files["file"];
                EmailReader emailReader = new EmailReader();
                dynamic result = emailReader.Read_email(file);

                return file != null
                    ? (ActionResult)new OkObjectResult(result)
                    : new BadRequestObjectResult("Sorry didn't get it all....");
            }catch(Exception e)
            {
                return (ActionResult)new OkObjectResult(e.ToString());
            }   

        }



    }

}
    