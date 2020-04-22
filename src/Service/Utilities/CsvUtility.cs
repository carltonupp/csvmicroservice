using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Service.Utilities
{
    internal static class CsvUtility
    {
        public static byte[] Create(IEnumerable<JObject> records, bool includeHeaders)
        {
            var sb = new StringBuilder();

            if (includeHeaders)
            {
                var headerRow = WriteHeaders(records.GetProperties());
                sb.AppendLine(headerRow);
            }

            foreach (var record in records)
            {
                sb.WriteRecord(record);
            }

            return sb.WriteToCsvFile();
        }

        private static string WriteHeaders(IEnumerable<JProperty> headers)
        {
            var sb = new StringBuilder();

            foreach (var header in headers)
            {
                sb.Append($"{header.Name}, ");
            }

            var headerRow = sb
                .ToString()
                .RemoveTrailingComma();

            return headerRow;
        }

        private static byte[] WriteToCsvFile(this StringBuilder builder)
        {
            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream);

            writer.Write(builder.ToString());
            writer.Flush();
            memoryStream.Position = 0;
            return memoryStream.ToArray();
        }

        private static StringBuilder WriteRecord(this StringBuilder builder, JObject record)
        {
            var rowBuilder = new StringBuilder();

            foreach (var property in record.Properties())
            {
                rowBuilder.Append($"{record.GetValue(property.Name)}, ");
            }

            var recordRow = rowBuilder.ToString().RemoveTrailingComma();
            builder.AppendLine(recordRow);
            return builder;
        }

        // Helper methods
        private static string RemoveTrailingComma(this string row) => row.Remove(row.Length - 2);
        private static IEnumerable<JProperty> GetProperties(this IEnumerable<JObject> objects) => objects.First().Properties();
    }
}
