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
        VerifierSettings.RegisterFileConverter<WebDriver>(DriverToImage);
        VerifierSettings.RegisterFileConverter<IWebElement>(ElementToImage);
    }

    static ConversionResult ElementToImage(IWebElement target, IReadOnlyDictionary<string, object> context)
    {
        var element = (WebElement)target;
        var driver = (WebDriver)element.WrappedDriver;
        driver.WaitForIsReady();
        var bytes = element.GetScreenshot().AsByteArray;
        var targets = new List<Target>();
        var source = element.GetSource();
        if (source != null)
        {
            targets.Add(new("html", source));
        }
        targets.Add(new("png", new MemoryStream(bytes)));
        return new(null, targets);
    }

    static ConversionResult DriverToImage(WebDriver driver, IReadOnlyDictionary<string, object> context)
    {
        driver.WaitForIsReady();
        var bytes = driver.GetScreenshot().AsByteArray;
        return new(
            null,
            [
                new("html", driver.PageSource),
                new("png", new MemoryStream(bytes))
            ]
        );
    }
}