public static class ModuleInitializer
{
    #region SeleniumEnable

    [ModuleInitializer]
    public static void InitSelenium() =>
        VerifySelenium.Enable();

    #endregion

    #region PlaywrightEnable

    [ModuleInitializer]
    public static void InitPlaywright() =>
        VerifyPlaywright.Enable(installPlaywright: true);

    #endregion

    #region PuppeteerEnable

    [ModuleInitializer]
    public static void InitPuppeteer() =>
        VerifyPuppeteer.Enable();

    #endregion

    [ModuleInitializer]
    public static void InitOther()
    {
        VerifyDiffPlex.Initialize();
        VerifyImageMagick.Initialize();
        VerifyImageMagick.RegisterComparers(.05);
    }
}