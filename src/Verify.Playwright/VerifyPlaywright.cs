using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PlaywrightSharp;

namespace VerifyTests
{
    public static class VerifyPlaywright
    {
        public static void Enable()
        {
            VerifierSettings.RegisterFileConverter<IPage>(PageToImage);
            VerifierSettings.RegisterFileConverter<IElementHandle>(ElementToImage);
        }

        static async Task<ConversionResult> PageToImage(IPage page, IReadOnlyDictionary<string, object> context)
        {
            await page.WaitForLoadStateAsync(LifecycleEvent.Networkidle);
            var bytes = await page.ScreenshotAsync();
            var html = await page.GetContentAsync();
            return new(
                null,
                new List<ConversionStream>
                {
                    new("html", html),
                    new("png", new MemoryStream(bytes))
                }
            );
        }

        static async Task<ConversionResult> ElementToImage(IElementHandle element, IReadOnlyDictionary<string, object> context)
        {
            var bytes = await element.ScreenshotAsync();
            var html = await element.GetInnerHtmlAsync();
            return new(
                null,
                new List<ConversionStream>
                {
                    new("html", html),
                    new("png", new MemoryStream(bytes))
                }
            );
        }
    }
}