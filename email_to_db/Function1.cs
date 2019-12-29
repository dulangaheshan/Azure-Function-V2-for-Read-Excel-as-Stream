
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

namespace email_to_db
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            //dynamic formdata = req.ReadFormAsync();
            dynamic file = req.Form.Files["file"];

            //dynamic file =  new StreamReader(req.Body).ReadToEndAsync();
            dynamic byte_obj = Convert_to_byte(file);
            //Byte excel_book = (WorkBook)file;
            List<string> rows = new List<string>();
            System.Text.Encoding.RegisterProvider(provider: System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream(byte_obj))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var value = reader.GetValue(i)?.ToString();
                            rows.Add(value);
                           
                        }
                    }
                }
            }

            return file != null
                ? (ActionResult)new OkObjectResult(rows)
                : new BadRequestObjectResult("Sorry didn't get it all....");
        }


        public static dynamic Convert_to_byte(dynamic file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();

                return fileBytes;
                // string s = Convert.ToBase64String(fileBytes);
                // act on the Base64 data
            }
        }


    }

}
    