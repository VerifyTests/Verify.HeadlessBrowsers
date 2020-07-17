using System;
using System.IO;
using System.Text;
using AngleSharp.Html.Parser;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

static class SeleniumExtensions
{
    public static void WaitForIsReady(this RemoteWebDriver driver)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(_ =>
        {
            var readyState = driver.ExecuteScript("return document.readyState").ToString();
            return readyState == "complete";
        });
    }

    public static Stream GetSource(this IWebElement element)
    {
        var html = element.GetAttribute("outerHTML");
        return CleanSource(html);
    }

    public static Stream GetSource(this RemoteWebDriver element)
    {
        return CleanSource(element.PageSource);
    }

    static Stream CleanSource(string html)
    {
        var parser = new HtmlParser();
        var document = parser.ParseFragment(html, null);

        var stream = new MemoryStream();
        using var writer = new StreamWriter(stream, Encoding.UTF8, 1000, true);
        document.ToHtml(writer, new MarkupFormatter());
        return stream;
    }
}