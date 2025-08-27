namespace VerifyTests;

public static class VerifySelenium
{
    public static bool Initialized { get; private set; }

    public static void Initialize()
    {
        if (Initialized)
        {
            throw new("Already Initialized");
        }

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();
        VerifierSettings.RegisterFileConverter<WebDriver>(ElementConverter.ConvertDriver);
        VerifierSettings.RegisterFileConverter<IWebElement>(ElementConverter.ConvertElement);
    }
}