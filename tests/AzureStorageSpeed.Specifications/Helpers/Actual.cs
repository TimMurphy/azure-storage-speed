using System;
using AzureStorageSpeed.Library;

namespace AzureStorageSpeed.Specifications.Helpers
{
    public class Actual
    {
        public SpeedTestResult[] SpeedTestResults { get; set; }
        public Exception Exception { get; set; }

        public void Run(Action action)
        {
            Exception = null;

            try
            {
                action();
            }
            catch (Exception exception)
            {
                Exception = exception;
            }
        }
    }
}