namespace CsvMicroservice.Core.Classes
{
    public sealed class CsvData
    {
        public dynamic[] Records { get; set; }
        public bool IncludeHeaders { get; set; }
    }
}
