namespace VerifyTests;

public static partial class VerifyPlaywright
{
    public static SettingsTask LocatorScreenshotOptions(this SettingsTask settings, LocatorScreenshotOptions options)
    {
        settings.CurrentSettings.LocatorScreenshotOptions(options);
        return settings;
    }

    internal static LocatorScreenshotOptions GetLocatorScreenshotOptions(this IReadOnlyDictionary<string, object> context)
    {
        if (context.TryGetValue("Playwright.LocatorScreenshotOptions", out var value))
        {
            var options = (LocatorScreenshotOptions)value;
            ValidateNoPath(options.Path);
            return options;
        }

        return new()
        {
            Type = ScreenshotType.Png
        };
    }

    static void LocatorScreenshotOptions(this VerifySettings settings, LocatorScreenshotOptions options) =>
        settings.Context["Playwright.LocatorScreenshotOptions"] = options;

    public static SettingsTask LocatorScreenshotOptions(this SettingsTask settings, LocatorScreenshotOptions options, bool screenshotOnly = false)
    {
        settings.CurrentSettings.LocatorScreenshotOptions(options);
        settings.CurrentSettings.Context["Playwright.ScreenshotOnly"] = screenshotOnly;
        return settings;
    }

    internal static bool GetScreenshotOnlyOption(this IReadOnlyDictionary<string, object> context)
    {
        if (context.TryGetValue("Playwright.ScreenshotOnly", out var value))
        {
            return (bool)value;
        }

        return false;
    }
}