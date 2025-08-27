static class ElementConverter
{
    public static ConversionResult ConvertElement(IWebElement target, IReadOnlyDictionary<string, object> context)
    {
        var element = (WebElement)target;
        var driver = (WebDriver)element.WrappedDriver;
        driver.WaitForIsReady();
        var bytes = element.GetScreenshot().AsByteArray;
        var targets = new List<Target>();
        var source = element.GetSource();
        if (source != null)
        {
            targets.Add(new("html", source));
        }

        targets.Add(new("png", new MemoryStream(bytes)));
        return new(null, targets);
    }

    public static ConversionResult ConvertDriver(WebDriver driver, IReadOnlyDictionary<string, object> context)
    {
        driver.WaitForIsReady();
        var bytes = driver.GetScreenshot().AsByteArray;
        return new(
            null,
            [
                new("html", driver.PageSource),
                new("png", new MemoryStream(bytes))
            ]
        );
    }
}