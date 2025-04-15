namespace VerifyTests;

public static class VerifyPlaywright
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

        VerifierSettings.RegisterFileConverter<IPage>(PageToImage);
        VerifierSettings.RegisterFileConverter<IElementHandle>(ElementToImage);
        VerifierSettings.RegisterFileConverter<ILocator>(LocatorToImage);
    }

    static async Task<ConversionResult> PageToImage(IPage page, IReadOnlyDictionary<string, object> context)
    {
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        Task<byte[]> bytes;
        var imageType = "png";
        if (context.GetPageScreenshotOptions(out var options))
        {
            bytes = page.ScreenshotAsync(options);
            if (options.Type == ScreenshotType.Jpeg)
            {
                imageType = "jpg";
            }
        }
        else
        {
            bytes = page.ScreenshotAsync(
                new()
                {
                    FullPage = true,
                    Type = ScreenshotType.Png
                });
        }

        await RemovePlaywrightStyle(page);

        var targets = new List<Target>();
        if (!context.GetScreenshotOnlyOption())
        {
            var html = await page.ContentAsync();
            targets.Add(new("html", html));
        }

        targets.Add(new(imageType, new MemoryStream(await bytes)));

        return new(null, targets);
    }

    static async Task RemovePlaywrightStyle(IPage page)
    {
        var elements = await page.QuerySelectorAllAsync("style");
        foreach (var element in elements)
        {
            var value = await element.InnerHTMLAsync();
            if (value.Contains("*:not(#playwright"))
            {
                await element.EvaluateAsync("element => element.remove()", element);
            }
        }
    }

    static async Task<ConversionResult> ElementToImage(IElementHandle element, IReadOnlyDictionary<string, object> context)
    {
        Task<byte[]> bytes;
        var imageType = "png";
        if (context.GetElementScreenshotOptions(out var options))
        {
            bytes = element.ScreenshotAsync(options);
            if (options.Type == ScreenshotType.Jpeg)
            {
                imageType = "jpg";
            }
        }
        else
        {
            bytes = element.ScreenshotAsync(
                new()
                {
                    Type = ScreenshotType.Png
                });
        }

        var targets = new List<Target>();
        if (!context.GetScreenshotOnlyOption())
        {
            var html = await element.InnerHTMLAsync();
            targets.Add(new("html", html));
        }

        targets.Add(new(imageType, new MemoryStream(await bytes)));

        return new(null, targets);
    }

    public static void PageScreenshotOptions(this VerifySettings settings, PageScreenshotOptions options) =>
        settings.Context["Playwright.PageScreenshotOptions"] = options;

    public static SettingsTask PageScreenshotOptions(this SettingsTask settings, PageScreenshotOptions options, bool screenshotOnly = false)
    {
        settings.CurrentSettings.PageScreenshotOptions(options);
        settings.CurrentSettings.Context["Playwright.ScreenshotOnly"] = screenshotOnly;
        return settings;
    }

    static bool GetPageScreenshotOptions(this IReadOnlyDictionary<string, object> context, [NotNullWhen(true)] out PageScreenshotOptions? options)
    {
        if (context.TryGetValue("Playwright.PageScreenshotOptions", out var value))
        {
            options = (PageScreenshotOptions) value;
            ValidateNoPath(options.Path);
            return true;
        }

        options = null;
        return false;
    }

    static void ValidateNoPath(string? path)
    {
        if (path != null)
        {
            throw new("ScreenshotOptions Path not supported.");
        }
    }

    public static void ElementScreenshotOptions(this VerifySettings settings, ElementHandleScreenshotOptions options) =>
        settings.Context["Playwright.ElementScreenshotOptions"] = options;

    public static SettingsTask ElementScreenshotOptions(this SettingsTask settings, ElementHandleScreenshotOptions options, bool screenshotOnly = false)
    {
        settings.CurrentSettings.ElementScreenshotOptions(options);
        settings.CurrentSettings.Context["Playwright.ScreenshotOnly"] = screenshotOnly;
        return settings;
    }

    static bool GetElementScreenshotOptions(this IReadOnlyDictionary<string, object> context, [NotNullWhen(true)] out ElementHandleScreenshotOptions? options)
    {
        if (context.TryGetValue("Playwright.ElementScreenshotOptions", out var value))
        {
            options = (ElementHandleScreenshotOptions) value;
            ValidateNoPath(options.Path);
            return true;
        }

        options = null;
        return false;
    }

    public static SettingsTask LocatorScreenshotOptions(this SettingsTask settings, LocatorScreenshotOptions options, bool screenshotOnly = false)
    {
        settings.CurrentSettings.LocatorScreenshotOptions(options);
        settings.CurrentSettings.Context["Playwright.ScreenshotOnly"] = screenshotOnly;
        return settings;
    }

    static bool GetScreenshotOnlyOption(this IReadOnlyDictionary<string, object> context)
    {
        if (context.TryGetValue("Playwright.ScreenshotOnly", out var value))
        {
            return (bool) value;
        }

        return false;
    }

    public static SettingsTask LocatorScreenshotOptions(this SettingsTask settings, LocatorScreenshotOptions options)
    {
        settings.CurrentSettings.LocatorScreenshotOptions(options);
        return settings;
    }

    static bool GetLocatorScreenshotOptions(this IReadOnlyDictionary<string, object> context, [NotNullWhen(true)] out LocatorScreenshotOptions? options)
    {
        if (context.TryGetValue("Playwright.LocatorScreenshotOptions", out var value))
        {
            options = (LocatorScreenshotOptions) value;
            ValidateNoPath(options.Path);
            return true;
        }

        options = null;
        return false;
    }

    static void LocatorScreenshotOptions(this VerifySettings settings, LocatorScreenshotOptions options) =>
        settings.Context["Playwright.LocatorScreenshotOptions"] = options;

    static async Task<ConversionResult> LocatorToImage(ILocator locator, IReadOnlyDictionary<string, object> context)
    {
        Task<byte[]> bytes;
        var imageType = "png";
        if (context.GetLocatorScreenshotOptions(out var options))
        {
            bytes = locator.ScreenshotAsync(options);
            if (options.Type == ScreenshotType.Jpeg)
            {
                imageType = "jpg";
            }
        }
        else
        {
            bytes = locator.ScreenshotAsync(
                new()
                {
                    Type = ScreenshotType.Png
                });
        }

        var targets = new List<Target>();
        if (!context.GetScreenshotOnlyOption())
        {
            var html = await locator.InnerHTMLAsync();
            targets.Add(new("html", html));
        }

        targets.Add(new(imageType, new MemoryStream(await bytes)));

        return new(null, targets);
    }
}