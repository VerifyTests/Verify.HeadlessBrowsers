public static class ModuleInitializer
{
    #region SeleniumEnable

    [ModuleInitializer]
    public static void InitSelenium() =>
        VerifySelenium.Initialize();

    #endregion

    #region PlaywrightEnable

    [ModuleInitializer]
    public static void InitPlaywright() =>
        VerifyPlaywright.Initialize(installPlaywright: true);

    #endregion

    #region PuppeteerEnable

    [ModuleInitializer]
    public static void InitPuppeteer() =>
        VerifyPuppeteer.Initialize();

    #endregion

    [ModuleInitializer]
    public static void InitOther()
    {
        VerifyImageMagick.RegisterComparers(.12);
        VerifierSettings.InitializePlugins();
    }
}