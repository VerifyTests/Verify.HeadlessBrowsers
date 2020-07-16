using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AngleSharp.Html.Parser;
using OpenQA.Selenium.Remote;

namespace VerifyTests
{
    public static class VerifySelenium
    {

        public static void Enable()
        {
            VerifierSettings.RegisterFileConverter<RemoteWebDriver>(DriverToImage);
            //VerifierSettings.RegisterFileConverter<IAppResult>(AppResultToImage);
        }

        static ConversionResult DriverToImage(RemoteWebDriver driver, VerifySettings settings)
        {
            driver.WaitForIsReady();
            var bytes = driver.GetScreenshot().AsByteArray;
            return new ConversionResult(
                null,
                new List<ConversionStream>
                {
                    new ConversionStream("html", driver.PageSource),
                    new ConversionStream("png", new MemoryStream(bytes))
                }
            );
        }

        //static ConversionResult AppResultToImage(IAppResult appResult, VerifySettings settings)
        //{
        //    var name = appResult.GetType().Name;
        //    if (name == "SeleniumAppResult")
        //    {
        //        var element = (RemoteWebElement) appResultSourceProperty.GetValue(appResult);

        //        var memoryStream = element.GetSource();
        //        return new ConversionResult(
        //            null,
        //            new List<ConversionStream>
        //            {
        //                new ConversionStream("html", memoryStream),
        //                new ConversionStream("png", element.TakeScreenshot())
        //            });
        //    }

        //    throw NotSupported();
        //}


        //static ConversionResult AppToImage(IApp app, VerifySettings settings)
        //{
        //    if (app is SeleniumApp seleniumApp)
        //    {
        //        var driver = (RemoteWebDriver) appDriverField.GetValue(seleniumApp);
        //        driver.WaitForIsReady();
        //        var unoGrid = driver.FindElement(unoMainPageClass);
        //        var bytes = driver.GetScreenshot().AsByteArray;
        //        return new ConversionResult(
        //            null,
        //            new List<ConversionStream>
        //            {
        //                new ConversionStream("html", unoGrid.GetSource()),
        //                new ConversionStream("png", new MemoryStream(bytes))
        //            }
        //        );
        //    }

        //    throw NotSupported();
        //}

        static Exception NotSupported()
        {
            return new Exception("Currently only Web apps via SeleniumApp are supported.");
        }
    }
}