using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

static class SeleniumExtensions
{
    public static void WaitForIsReady(this RemoteWebDriver driver)
    {
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
        wait.Until(_ =>
        {
            var readyState = driver.ExecuteScript("return document.readyState").ToString();
            return readyState == "complete";
        });
    }

    public static string GetSource(this IWebElement element)
    {
        return element.GetAttribute("outerHTML");
    }
}