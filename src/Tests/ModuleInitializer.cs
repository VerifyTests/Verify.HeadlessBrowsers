public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        #region SeleniumEnable

        VerifySelenium.Enable();

        #endregion

        #region PlaywrightEnable

        VerifyPlaywright.Enable(installPlaywright: true);

        #endregion

        #region PuppeteerEnable

        VerifyPuppeteer.Enable();

        #endregion

        VerifyDiffPlex.Initialize();
        VerifyImageMagick.Initialize();
        VerifyImageMagick.RegisterComparers(.05);
    }
}