namespace VerifyTests;

public static partial class VerifyPlaywright
{
    public static SettingsTask LocatorScreenshotOptions(this SettingsTask settings, LocatorScreenshotOptions options)
    {
        settings.CurrentSettings.LocatorScreenshotOptions(options);
        return settings;
    }

    internal static bool GetLocatorScreenshotOptions(this IReadOnlyDictionary<string, object> context, [NotNullWhen(true)] out LocatorScreenshotOptions? options)
    {
        if (context.TryGetValue("Playwright.LocatorScreenshotOptions", out var value))
        {
            options = (LocatorScreenshotOptions)value;
            ValidateNoPath(options.Path);
            return true;
        }

        options = null;
        return false;
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