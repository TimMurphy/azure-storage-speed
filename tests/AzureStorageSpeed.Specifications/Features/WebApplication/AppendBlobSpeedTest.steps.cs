using AzureStorageSpeed.Specifications.Helpers;
using TechTalk.SpecFlow;

namespace AzureStorageSpeed.Specifications.Features.WebApplication
{
    [Binding]
    public class AppendBlobSpeedTestSteps : WebApplicationSteps
    {
        public AppendBlobSpeedTestSteps(Given given, Actual actual)
            : base(given, actual)
        {
        }
    }
}