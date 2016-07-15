using System;
using Anotar.LibLog;
using OpenMagic.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using TechTalk.SpecFlow;

namespace AzureStorageSpeed.Specifications.Helpers
{
    [Binding]
    public class WebDriverSteps
    {
        public static readonly Lazy<IWebDriver> LazyWebDriver = new Lazy<IWebDriver>(WebDriverFactory);

        [AfterTestRun]
        public static void AfterAllTests()
        {
            LazyWebDriver.DisposeValueIfCreated();
        }

        private static IWebDriver WebDriverFactory()
        {
            LogTo.Info("Creating web driver");
            //return new FirefoxDriver();
            return new PhantomJSDriver();
        }
    }
}