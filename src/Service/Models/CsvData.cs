namespace Service.Models
{
    public sealed class CsvData
    {
        public dynamic[] Records { get; set; }
        public bool IncludeHeaders { get; set; }
        public string? FileName { get; set; }
    }
}
