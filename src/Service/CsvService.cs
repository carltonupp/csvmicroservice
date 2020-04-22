using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.Models;
using System.Linq;
using Service.Utilities;
using Newtonsoft.Json.Linq;

namespace Service
{
    public static class CsvService
    {
        [FunctionName("ConvertToCsv")]
        public static async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            using (var streamReader = new StreamReader(req.Body))
            {
                var body = await streamReader.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<CsvData>(body);

                var records = data.Records
                    .Select(record => JsonConvert.DeserializeObject(record.ToString()))
                    .Cast<JObject>();

                var csv = CsvUtility.Create(records, data.IncludeHeaders);

                return new FileContentResult(csv, "application/octet-stream")
                {
                    FileDownloadName = data.FileName ?? "Results.csv"
                };
            }
        }
    }
}
