using System;
using System.IO;
using System.Text;
using AngleSharp.Html;
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

    public static string GetSource(this IWebElement element)
    {
        var html = element.GetAttribute("outerHTML");
        return CleanSource(html);
    }

    public static string GetSource(this RemoteWebDriver element)
    {
        return CleanSource(element.PageSource);
    }

    static PrettyMarkupFormatter formatter = new()
    {
        Indentation = "  ",
        NewLine = "\n"
    };

    static string CleanSource(string html)
    {
        HtmlParser parser = new();
        var document = parser.ParseFragment(html, null);

        StringBuilder builder = new();
        using StringWriter writer = new(builder);
        document.ToHtml(writer, formatter);
        return builder.ToString();
    }
}