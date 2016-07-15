namespace AzureStorageSpeed.Specifications.Helpers
{
    public class Given
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
        public int StringLength { get; set; }
        public int Rows { get; set; }
        public string ContainerName { get; set; }
        public string BlobName { get; set; }
    }
}