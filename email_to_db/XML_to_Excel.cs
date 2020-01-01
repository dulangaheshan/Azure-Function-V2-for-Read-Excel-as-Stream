using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace email_to_db
{
    public static class XML_to_Excel
    {
        
        [FunctionName("XML_to_Excel")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            dynamic result = null;
            log.LogInformation("C# HTTP trigger function processed a request.");

            string file = req.Query["file"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            file = file ?? data?.file;
            //byte[] buffer = (new UnicodeEncoding()).GetBytes(file);



            //var bytes = Encoding.UTF8.GetBytes(file);
            ////for (int i = 0; i < bytes.Length; i++) {
            ////    bytes[i] ^= 0x5a;
            ////}
            // File.WriteAllText("abcd.xlsx", System.Web.HttpUtility.UrlDecode(file));
            //dynamic file2 = Convert.FromBase64String(file);
            //myBase64String2 = Convert.ToBase64String(file);

            try
            {
                byte[] fileinbytes = Convert.FromBase64String(file);
                //File.WriteAllBytes("abcd.xlsx", fileinbytes);
                EmailReader emailReader = new EmailReader();
                result = emailReader.byte_array_to_datatable(fileinbytes);


                return file != null
                    ? (ActionResult)new OkObjectResult(result)
                    : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
            } catch (Exception e)
            {
                return (ActionResult)new OkObjectResult(e.ToString());
            }


        }
    }
}
