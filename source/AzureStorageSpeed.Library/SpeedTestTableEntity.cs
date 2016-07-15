using Microsoft.WindowsAzure.Storage.Table;

namespace AzureStorageSpeed.Library
{
    public class SpeedTestTableEntity : TableEntity
    {
        public SpeedTestTableEntity()
        {
        }

        public SpeedTestTableEntity(string partitionKey, int testNumber, string stringValue)
            : base(partitionKey, testNumber.ToString("D19"))
        {
            StringValue = stringValue;
        }

        public string StringValue { get; set; }
    }
}