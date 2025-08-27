static class ElementConverter
{
    public static async Task<ConversionResult> ConvertPage(IPage page, IReadOnlyDictionary<string, object> context)
    {
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        var options = context.GetPageScreenshotOptions();
        var bytes = page.ScreenshotAsync(options);

        await page.RemovePlaywrightStyle();

        var targets = new List<Target>();
        if (!context.GetScreenshotOnlyOption())
        {
            var html = await page.ContentAsync();
            targets.Add(new("html", html));
        }

        targets.Add(new(options.ToExtension(), new MemoryStream(await bytes)));

        return new(null, targets);
    }

    public static async Task<ConversionResult> ConvertElement(IElementHandle element, IReadOnlyDictionary<string, object> context)
    {
        var options = context.GetElementScreenshotOptions();
        var bytes = element.ScreenshotAsync(options);

        var targets = new List<Target>();
        if (!context.GetScreenshotOnlyOption())
        {
            var html = await element.InnerHTMLAsync();
            targets.Add(new("html", html));
        }

        targets.Add(new(options.ToExtension(), new MemoryStream(await bytes)));

        return new(null, targets);
    }

    public static async Task<ConversionResult> ConvertLocator(ILocator locator, IReadOnlyDictionary<string, object> context)
    {
        var options = context.GetLocatorScreenshotOptions();
        var bytes = locator.ScreenshotAsync(options);

        var targets = new List<Target>();
        if (!context.GetScreenshotOnlyOption())
        {
            var html = await locator.InnerHTMLAsync();
            targets.Add(new("html", html));
        }

        targets.Add(new(options.ToExtension(), new MemoryStream(await bytes)));

        return new(null, targets);
    }

    static string ToExtension(this PageScreenshotOptions options) =>
        options.Type == ScreenshotType.Jpeg ? "jpg" : "png";

    static string ToExtension(this ElementHandleScreenshotOptions options) =>
        options.Type == ScreenshotType.Jpeg ? "jpg" : "png";

    static string ToExtension(this LocatorScreenshotOptions options) =>
        options.Type == ScreenshotType.Jpeg ? "jpg" : "png";
}