using AzureStorageSpeed.Specifications.Helpers;
using FluentAssertions;
using OpenMagic.Extensions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AzureStorageSpeed.Specifications.Features.WebApplication
{
    [Binding]
    public class TableSpeedTestSteps : WebApplicationSteps
    {
        public TableSpeedTestSteps(Given given, Actual actual)
            : base(given, actual)
        {
        }

        [Given(@"I have navigated to (.*)")]
        public void GivenIHaveNavigatedTo(string resource)
        {
            WebDriver.NavigateTo($"http://localhost:7911/{resource.TrimStart("/")}");
        }

        [Given(@"fill in the form with")]
        public void GivenFillInTheFormWith(Table table)
        {
            foreach (var tableRow in table.Rows)
            {
                var id = tableRow["Id"];
                var text = tableRow["Text"];

                if (id == "ConnectionString" && text == "secret")
                {
                    text = AppSettings.ConnectionString;
                }

                var element = WebDriver.FindElement(By.Id(id));

                element.Clear();
                element.SendKeys(text);
            }
        }

        [When(@"I press (.*)")]
        public void WhenIPress(string buttonText)
        {
            var id = buttonText.Replace(" ", "");
            var element = WebDriver.FindElement(By.Id(id));

            element.Click();
        }

        [Then(@"the speed test results should be displayed")]
        public void ThenTheSpeedTestResultsShouldBeDisplayed()
        {
            const string id = "results";
            var elements = WebDriver.FindElements(By.Id(id));

            elements.Count.Should().Be(1, "because results div should exist");
        }
    }
}