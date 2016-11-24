namespace ServiceStatus
{
    public class SchemaVersion
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public string Version => $"{Major}.{Minor}";
    }
}
