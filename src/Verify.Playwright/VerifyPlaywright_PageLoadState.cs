namespace VerifyTests;

public static partial class VerifyPlaywright
{
    public static void PageLoadState(this VerifySettings settings, LoadState? state, PageWaitForLoadStateOptions? options = null) =>
        settings.Context["Playwright.PageLoadState"] = new PageLoadStateSetting(state, options);

    public static SettingsTask PageLoadState(this SettingsTask settings, LoadState? state, PageWaitForLoadStateOptions? options = null)
    {
        settings.CurrentSettings.PageLoadState(state, options);
        return settings;
    }

    internal static Task WaitForLoadState(this IReadOnlyDictionary<string, object> context, IPage page)
    {
        if (!context.TryGetValue("Playwright.PageLoadState", out var value))
        {
            return page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        var setting = (PageLoadStateSetting)value;
        if (setting.State == null)
        {
            return Task.CompletedTask;
        }

        return page.WaitForLoadStateAsync(setting.State, setting.Options);
    }

    record PageLoadStateSetting(LoadState? State, PageWaitForLoadStateOptions? Options);
}
