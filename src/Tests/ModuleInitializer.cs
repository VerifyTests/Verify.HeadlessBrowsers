using System.Runtime.CompilerServices;
using ImageMagick;
using VerifyTests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        #region SeleniumEnable

        VerifySelenium.Enable();

        #endregion

        #region PlaywrightEnable

        VerifyPlaywright.Enable();

        #endregion

        #region PuppeteerEnable

        VerifyPuppeteer.Enable();

        #endregion

        VerifyImageMagick.Initialize();
        VerifyImageMagick.RegisterComparers(.01, ErrorMetric.Fuzz);
    }
}