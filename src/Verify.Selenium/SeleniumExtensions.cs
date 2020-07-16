using System;
using System.Drawing;
using System.Drawing.Imaging;
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

    public static Stream TakeScreenshot(this RemoteWebElement element)
    {
        var bytes = ((ITakesScreenshot) element.WrappedDriver).GetScreenshot().AsByteArray;
        using var inputStream = new MemoryStream(bytes);
        var screenshot = new Bitmap(inputStream);
        var cropSize = new Rectangle(element.Location.X, element.Location.Y, element.Size.Width, element.Size.Height);
        using var cropped = screenshot.Clone(cropSize, screenshot.PixelFormat);
        var outputStream = new MemoryStream();
        cropped.Save(outputStream, ImageFormat.Png);
        return outputStream;
    }
    public  static Stream GetSource(this IWebElement element)
        {
            var parser = new HtmlParser();
            var innerHtml = element.GetAttribute("outerHTML");
            var document = parser.ParseFragment(innerHtml, null);

            //foreach (var node in document)
            //{
            //    Sanitize(node);
            //}
            var stream = new MemoryStream();
            using var writer = new StreamWriter(stream, Encoding.UTF8, 1000, true);
            document.ToHtml(writer, new MarkupFormatter());
            return stream;
        }

        //static void Sanitize(INode node)
        //{
        //    if (node is IElement htmlElement)
        //    {
        //        for (var i = htmlElement.Attributes.Length - 1; i >= 0; i--)
        //        {
        //            var attribute = htmlElement.Attributes[i];

        //            var value = attribute.Value;
        //            if (ShouldRemoveAttribute(attribute))
        //            {
        //                htmlElement.RemoveAttribute(attribute.NamespaceUri, attribute.Name);
        //                continue;
        //            }

        //            if (attribute.Name == "style")
        //            {
        //                attribute.Value = string.Join("; ",
        //                    value
        //                        .Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
        //                        .Select(x => x.Trim())
        //                        .Where(x =>
        //                        {
        //                            return !string.IsNullOrWhiteSpace(x) &&
        //                                   x != "color: rgb(0, 0, 0)" &&
        //                                   x != "background-color: rgb(255, 255, 255)" &&
        //                                   x != "font-style: normal" &&
        //                                   x != "font-weight: normal" &&
        //                                   x != "text-align: left" &&
        //                                   x != "position: absolute" &&
        //                                   x != "white-space: pre" &&
        //                                   x != "top: 0px" &&
        //                                   x != "left: 0px" &&
        //                                   x != "letter-spacing: 0em" &&
        //                                   x != "font-family: \"Segoe UI\"" &&
        //                                   x != "pointer-events: none" &&
        //                                   x != "pointer-events: auto";
        //                        }));
        //            }
        //        }
        //    }

        //    for (var i = node.ChildNodes.Length - 1; i >= 0; i--)
        //    {
        //        Sanitize(node.ChildNodes[i]);
        //    }
        //}

        //static bool ShouldRemoveAttribute(IAttr attribute)
        //{
        //    var name = attribute.Name;
        //    var value = attribute.Value;
        //    return name == "xamlhandle" ||
        //           name == "id" ||
        //           (name == "tabindex" && value == "-1");
        //}
}