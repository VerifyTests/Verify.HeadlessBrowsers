namespace VerifyTests;

public static partial class VerifyPlaywright
{
    public static bool Initialized { get; private set; }

    public static void Initialize(bool installPlaywright = false)
    {
        if (Initialized)
        {
            throw new("Already Initialized");
        }

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();
        if (installPlaywright)
        {
            Program.Main(["install"]);
        }

        VerifierSettings.RegisterFileConverter<IPage>(ImageBuilder.PageToImage);
        VerifierSettings.RegisterFileConverter<IElementHandle>(ImageBuilder.ElementToImage);
        VerifierSettings.RegisterFileConverter<ILocator>(ImageBuilder.LocatorToImage);
    }

    static void ValidateNoPath(string? path)
    {
        if (path != null)
        {
            throw new("ScreenshotOptions Path not supported.");
        }
    }
}