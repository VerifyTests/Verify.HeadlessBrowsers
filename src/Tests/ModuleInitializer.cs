using System.Runtime.CompilerServices;
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

        #region VerifyPuppeteerEnable

        VerifyPuppeteer.Enable();

        #endregion

        VerifyPhash.RegisterComparer("png", .99f);
    }
}