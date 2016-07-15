using System.Configuration;

namespace AzureStorageSpeed.Specifications.Helpers
{
    public static class AppSettings
    {
        public static string ConnectionString => ConfigurationManager.AppSettings["Azure_Storage_ConnectionString"];
    }
}