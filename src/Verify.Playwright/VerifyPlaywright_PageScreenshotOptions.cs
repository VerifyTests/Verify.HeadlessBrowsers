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

    internal static PageScreenshotOptions GetPageScreenshotOptions(this IReadOnlyDictionary<string, object> context)
    {
        if (context.TryGetValue("Playwright.PageScreenshotOptions", out var value))
        {
            var options = (PageScreenshotOptions)value;
            ValidateNoPath(options.Path);
            return options;
        }

        return new()
        {
            FullPage = true,
            Type = ScreenshotType.Png
        };
    }
}