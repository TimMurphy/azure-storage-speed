using System.Linq;
using AzureStorageSpeed.Library;
using AzureStorageSpeed.Specifications.Helpers;
using TechTalk.SpecFlow;

namespace AzureStorageSpeed.Specifications.Features.Library
{
    [Binding]
    public class AppendBlobSpeedTestSteps : LibrarySteps
    {
        public AppendBlobSpeedTestSteps(Given given, Actual actual)
            : base(given, actual)
        {
        }

        [Given(@"containerName is (.*)")]
        public void GivenContainerNameIs(string containerName)
        {
            Given.ContainerName = containerName;
        }

        [Given(@"blobName is (.*)")]
        public void GivenBlobNameIs(string blobName)
        {
            Given.BlobName = blobName;
        }

        [When(@"I call AppendBlobSpeedTest\.RunAsync\(connectionString, containerName, blobName, stringLength, rows\)")]
        public void WhenICallAppendBlobSpeedTest_RunAsyncConnectionStringContainerNameBlobNameStringLengthRows()
        {
            Actual.Run(() => Actual.SpeedTestResults = new AppendBlobSpeedTest(Given.ConnectionString, Given.ContainerName, Given.BlobName, Given.StringLength, Given.Rows).RunAsync().Result.ToArray());
        }
    }
}