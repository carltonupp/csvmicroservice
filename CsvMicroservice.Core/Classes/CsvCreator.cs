using CsvMicroservice.Core.Utilities;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvMicroservice.Core.Classes
{
    public static class CsvCreator
    {
        public static byte[] Create(IEnumerable<JObject> data, bool includeHeaders)
        {
            var csv = new StringBuilder()
                .WriteHeaders(data.First().Properties(), includeHeaders)
                .WriteRecords(data)
                .WriteToCsvFile();
            return csv;
        }

        private static StringBuilder WriteHeaders(this StringBuilder builder, IEnumerable<JProperty> headers, bool includeHeaders)
        {
            if (includeHeaders)
            {
                var rowBuilder = new StringBuilder();
                foreach (var header in headers)
                {
                    rowBuilder.Append($"{header.Name}, ");
                }
                var headerRow = rowBuilder.ToString()
                    .RemoveTrailingComma();
                builder.AppendLine(headerRow);
            }

            return builder;
        }
        private static byte[] WriteToCsvFile(this StringBuilder builder)
        {
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            writer.Write(builder.ToString());
            writer.Flush();
            memoryStream.Position = 0;
            return memoryStream.ToArray();
        }

        private static StringBuilder WriteRecords(this StringBuilder builder, IEnumerable<JObject> records)
        {
            foreach (var record in records)
            {
                builder.WriteRecord(record);
            }
            return builder;
        }

        private static StringBuilder WriteRecord(this StringBuilder builder, JObject record)
        {
            var rowBuilder = new StringBuilder();

            foreach (var property in record.Properties())
            {
                rowBuilder.Append($"{record.GetValue(property.Name)}, ");
            }

            var recordRow = rowBuilder.ToString()
                .RemoveTrailingComma();
            builder.AppendLine(recordRow);
            return builder;
        }
        
    }
}
