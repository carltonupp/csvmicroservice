using CsvMicroservice.Core.Classes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CsvMicroservice.Api.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {

        [HttpPost]
        public IActionResult Create(CsvData data)
        {
            var records = new List<JObject>();

            foreach (var record in data.Records)
            {
                var recordJsonObject = JsonConvert.DeserializeObject(record.ToString());
                records.Add(recordJsonObject);
            }

            var csv = CsvCreator.Create(records, data.IncludeHeaders);
            return File(csv, "application/octet-stream", "Results.csv");
        }
    }
}