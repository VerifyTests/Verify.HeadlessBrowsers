namespace VerifyTests;

public static class VerifyPuppeteer
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
        VerifierSettings.RegisterFileConverter<ElementHandle>(ElementConverter.ConvertElement);
        VerifierSettings.RegisterFileConverter<Page>(ElementConverter.ConvertPage);
    }
}