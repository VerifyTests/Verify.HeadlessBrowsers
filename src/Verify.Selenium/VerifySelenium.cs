using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace VerifyTests
{
    public static class VerifySelenium
    {
        public static void Enable()
        {
            VerifierSettings.RegisterFileConverter<RemoteWebDriver>(DriverToImage);
            VerifierSettings.RegisterFileConverter<IWebElement>(ElementToImage);
        }

        static ConversionResult ElementToImage(IWebElement target, IReadOnlyDictionary<string, object> context)
        {
            var element = (RemoteWebElement)target;
            var driver = (RemoteWebDriver)element.WrappedDriver;
            driver.WaitForIsReady();
            var bytes = element.GetScreenshot().AsByteArray;
            return new(
                null,
                new List<Target>
                {
                    new("html", element.GetSource()),
                    new("png", new MemoryStream(bytes))
                }
            );
        }

        static ConversionResult DriverToImage(RemoteWebDriver driver, IReadOnlyDictionary<string, object> context)
        {
            driver.WaitForIsReady();
            var bytes = driver.GetScreenshot().AsByteArray;
            return new(
                null,
                new List<Target>
                {
                    new("html", driver.PageSource),
                    new("png", new MemoryStream(bytes))
                }
            );
        }
    }
}