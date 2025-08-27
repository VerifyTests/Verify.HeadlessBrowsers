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

        VerifierSettings.RegisterFileConverter<IPage>(ElementConverter.ConvertPage);
        VerifierSettings.RegisterFileConverter<IElementHandle>(ElementConverter.ConvertElement);
        VerifierSettings.RegisterFileConverter<ILocator>(ElementConverter.ConvertLocator);
    }

    static void ValidateNoPath(string? path)
    {
        if (path != null)
        {
            throw new("ScreenshotOptions Path not supported.");
        }
    }
}