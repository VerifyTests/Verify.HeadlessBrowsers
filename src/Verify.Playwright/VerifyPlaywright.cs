using Microsoft.Playwright;

namespace VerifyTests;

public static class VerifyPlaywright
{
    public static void Enable(bool installPlaywright = false)
    {
        if (installPlaywright)
        {
            Program.Main(new[]
            {
                "install"
            });
        }

        VerifierSettings.RegisterFileConverter<IPage>(PageToImage);
        VerifierSettings.RegisterFileConverter<IElementHandle>(ElementToImage);
    }

    static async Task<ConversionResult> PageToImage(IPage page, IReadOnlyDictionary<string, object> context)
    {
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var bytes = page.ScreenshotAsync();
        var html = await page.ContentAsync();
        html = html.Replace(playwrightStyle, "\n");
        return new(
            null,
            new List<Target>
            {
                new("html", html, null),
                new("png", new MemoryStream(await bytes), null)
            }
        );
    }

    static string playwrightStyle = @"<style>
            *:not(#playwright-aaaaaaaaaa.playwright-bbbbbbbbbbb.playwright-cccccccccc.playwright-dddddddddd.playwright-eeeeeeeee) {
              caret-color: transparent !important;
            }
          </style>"
        .Replace("\r\n", "\n");

    static async Task<ConversionResult> ElementToImage(IElementHandle element, IReadOnlyDictionary<string, object> context)
    {
        var bytes = element.ScreenshotAsync();
        var html = element.InnerHTMLAsync();
        return new(
            null,
            new List<Target>
            {
                new("html", await html, null),
                new("png", new MemoryStream(await bytes), null)
            }
        );
    }
}