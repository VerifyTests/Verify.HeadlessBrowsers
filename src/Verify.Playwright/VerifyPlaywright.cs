using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Playwright;

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
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var bytes = page.ScreenshotAsync();
            var html = page.ContentAsync();
            return new(
                null,
                new List<Target>
                {
                    new("html", await html),
                    new("png", new MemoryStream(await bytes))
                }
            );
        }

        static async Task<ConversionResult> ElementToImage(IElementHandle element, IReadOnlyDictionary<string, object> context)
        {
            var bytes = element.ScreenshotAsync();
            var html = element.InnerHTMLAsync();
            return new(
                null,
                new List<Target>
                {
                    new("html", await html),
                    new("png", new MemoryStream(await bytes))
                }
            );
        }
    }
}