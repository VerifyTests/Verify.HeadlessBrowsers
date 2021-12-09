using ImageMagick;

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
        Microsoft.Playwright.Program.Main(new[] {"install"});
        #endregion

        #region PuppeteerEnable

        VerifyPuppeteer.Enable();

        #endregion

        VerifyImageMagick.Initialize();
        VerifyImageMagick.RegisterComparers(.05, ErrorMetric.Fuzz);
    }
}