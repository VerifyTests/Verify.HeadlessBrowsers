namespace VerifyTests;

public static partial class VerifyPlaywright
{
    public static void PageScreenshotOptions(this VerifySettings settings, PageScreenshotOptions options) =>
        settings.Context["Playwright.PageScreenshotOptions"] = options;

    public static SettingsTask PageScreenshotOptions(this SettingsTask settings, PageScreenshotOptions options, bool screenshotOnly = false)
    {
        settings.CurrentSettings.PageScreenshotOptions(options);
        settings.CurrentSettings.Context["Playwright.ScreenshotOnly"] = screenshotOnly;
        return settings;
    }

    internal static bool GetPageScreenshotOptions(this IReadOnlyDictionary<string, object> context, [NotNullWhen(true)] out PageScreenshotOptions? options)
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
}