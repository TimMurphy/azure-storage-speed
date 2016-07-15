using System.Linq;
using Anotar.LibLog;
using AzureStorageSpeed.Library;
using AzureStorageSpeed.Specifications.Helpers;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace AzureStorageSpeed.Specifications.Features.Library
{
    [Binding]
    public class TableSpeedTestSteps : LibrarySteps
    {
        public TableSpeedTestSteps(Given given, Actual actual)
            : base(given, actual)
        {
        }

        [Given(@"connectionString is secret")]
        public void GivenConnectionStringIsSecret()
        {
            Given.ConnectionString = AppSettings.ConnectionString;
        }

        [Given(@"tableName is (.*)")]
        public void GivenTableNameIsSpeedtest(string tableName)
        {
            Given.TableName = tableName;
        }

        [Given(@"stringLength is (.*)")]
        public void GivenStringLengthIs(int stringLength)
        {
            Given.StringLength = stringLength;
        }

        [Given(@"rows (.*)")]
        public void GivenTests(int rows)
        {
            Given.Rows = rows;
        }

        [When(@"I call TableSpeedTest\.RunAsync\(connectionString, tableName, stringLength, rows\)")]
        public void When_I_Call_TableWriter_RunAsync_ConnectionString_TableName_StringLength_Rows()
        {
            Actual.Run(() => Actual.SpeedTestResults = new TableSpeedTest(Given.ConnectionString, Given.TableName, Given.StringLength, Given.Rows).RunAsync().Result.ToArray());
        }

        [Then(@"(.*) speed test results should be returned")]
        public void ThenSpeedTestResultsShouldBeReturned(int expectedSpeedTestResultsCount)
        {
            Actual.Exception.Should().BeNull();

            foreach (var speedTestResult in Actual.SpeedTestResults)
            {
                LogTo.Info($"{speedTestResult.Message} {speedTestResult.Elapsed.TotalMilliseconds:N0}ms");
            }

            Actual.SpeedTestResults.Length.Should().Be(expectedSpeedTestResultsCount, $"because {Given.Rows} tests were requested + setup + read");
            Actual.SpeedTestResults.First().Message.Should().Be("Setup");
            Actual.SpeedTestResults.Last().Message.Should().Be("Read");
        }
    }
}