static class SeleniumExtensions
{
    public static void WaitForIsReady(this WebDriver driver)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(_ =>
        {
            var readyState = driver.ExecuteScript("return document.readyState").ToString();
            return readyState == "complete";
        });
    }

    public static string GetSource(this IWebElement element) =>
#pragma warning disable CS0618 // Type or member is obsolete
        element.GetAttribute("outerHTML");
#pragma warning restore CS0618 // Type or member is obsolete
}