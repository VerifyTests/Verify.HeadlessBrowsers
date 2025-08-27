static class ElementConverter
{
    public static async Task<ConversionResult> ConvertPage(IPage page, IReadOnlyDictionary<string, object> context)
    {
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        var options = context.GetPageScreenshotOptions();
        var bytes = page.ScreenshotAsync(options);
        var imageType = options.Type == ScreenshotType.Jpeg ? "jpg" : "png";

        await page.RemovePlaywrightStyle();

        var targets = new List<Target>();
        if (!context.GetScreenshotOnlyOption())
        {
            var html = await page.ContentAsync();
            targets.Add(new("html", html));
        }

        targets.Add(new(imageType, new MemoryStream(await bytes)));

        return new(null, targets);
    }

    public static async Task<ConversionResult> ConvertElement(IElementHandle element, IReadOnlyDictionary<string, object> context)
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

    public static async Task<ConversionResult> ConvertLocator(ILocator locator, IReadOnlyDictionary<string, object> context)
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