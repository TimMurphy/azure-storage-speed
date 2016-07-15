using OpenQA.Selenium;

namespace AzureStorageSpeed.Specifications.Helpers
{
    public abstract class WebApplicationSteps : CommonSteps
    {
        protected WebApplicationSteps(Given given, Actual actual)
            : base(given, actual)
        {
        }

        protected IWebDriver WebDriver => WebDriverSteps.LazyWebDriver.Value;
    }
}