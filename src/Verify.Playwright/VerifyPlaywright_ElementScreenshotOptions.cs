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

    internal static bool GetElementScreenshotOptions(this IReadOnlyDictionary<string, object> context, [NotNullWhen(true)] out ElementHandleScreenshotOptions? options)
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
}