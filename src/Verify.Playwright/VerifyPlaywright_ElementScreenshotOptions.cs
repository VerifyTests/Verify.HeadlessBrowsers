namespace VerifyTests;

public static partial class VerifyPlaywright
{
    public static void ElementScreenshotOptions(this VerifySettings settings, ElementHandleScreenshotOptions options) =>
        settings.Context["Playwright.ElementScreenshotOptions"] = options;

    public static SettingsTask ElementScreenshotOptions(this SettingsTask settings, ElementHandleScreenshotOptions options, bool screenshotOnly = false)
    {
        settings.CurrentSettings.ElementScreenshotOptions(options);
        settings.CurrentSettings.Context["Playwright.ScreenshotOnly"] = screenshotOnly;
        return settings;
    }

    internal static ElementHandleScreenshotOptions GetElementScreenshotOptions(this IReadOnlyDictionary<string, object> context)
    {
        if (context.TryGetValue("Playwright.ElementScreenshotOptions", out var value))
        {
            var options = (ElementHandleScreenshotOptions) value;
            ValidateNoPath(options.Path);
            return options;
        }

        return new()
        {
            Type = ScreenshotType.Png
        };
    }
}