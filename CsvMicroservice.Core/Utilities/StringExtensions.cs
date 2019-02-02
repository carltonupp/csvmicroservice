namespace CsvMicroservice.Core.Utilities
{
    public static class StringExtensions
    {
        public static string RemoveTrailingComma(this string row) => row.Remove(row.Length - 2);
    }
}
