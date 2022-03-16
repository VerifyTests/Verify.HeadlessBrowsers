using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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
        element.GetAttribute("outerHTML");
}