static class ElementConverter
{
    public static async Task<ConversionResult> ConvertPage(Page page, IReadOnlyDictionary<string, object> context)
    {
        var screenshot = page.ScreenshotStreamAsync();
        var html = page.GetContentAsync();

        return new(
            null,
            [
                new("html", await html),
                new("png", await screenshot)
            ]
        );
    }

    public static async Task<ConversionResult> ConvertElement(ElementHandle element, IReadOnlyDictionary<string, object> context)
    {
        var screenshot = element.ScreenshotStreamAsync();
        var html = element.EvaluateFunctionAsync<string>("element => element.innerHTML");

        return new(
            null,
            [
                new("html", await html),
                new("png", await screenshot)
            ]
        );
    }
}