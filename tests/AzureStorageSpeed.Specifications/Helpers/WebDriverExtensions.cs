using System;
using OpenMagic.Extensions;
using OpenQA.Selenium;

namespace AzureStorageSpeed.Specifications.Helpers
{
    internal static class WebDriverExtensions
    {
        internal static void NavigateTo(this IWebDriver webDriver, string url)
        {
            ValidateUrlIsRunning(url);

            webDriver.Url = url;

            ValidatePageFound(webDriver, url);
        }

        private static void ValidatePageFound(IWebDriver webDriver, string url)
        {
            var source = webDriver.PageSource;

            if (source.Contains("HTTP 404"))
            {
                throw new Exception($"404 Page Not Found. {url}");
            }
        }

        private static void ValidateUrlIsRunning(string url)
        {
            var uri = new Uri(url);

            if (!uri.IsResponding())
            {
                // todo: automatically start website
                throw new Exception("Website is not running.");
            }
        }
    }
}