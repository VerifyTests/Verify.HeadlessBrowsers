using PuppeteerSharp;

namespace VerifyTests;

public static class VerifyPuppeteer
{
    public static void Enable()
    {
        VerifierSettings.RegisterFileConverter<ElementHandle>(ElementToImage);
        VerifierSettings.RegisterFileConverter<Page>(PageToImage);
    }

    static async Task<ConversionResult> PageToImage(Page page, IReadOnlyDictionary<string, object> context)
    {
        var screenshot = page.ScreenshotStreamAsync();
        var html = page.GetContentAsync();

        return new(
            null,
            new List<Target>
            {
                new("html", await html),
                new("png", await screenshot)
            }
        );
    }

    static async Task<ConversionResult> ElementToImage(ElementHandle element, IReadOnlyDictionary<string, object> context)
    {
        var screenshot = await element.ScreenshotStreamAsync();
        var html = await element.EvaluateFunctionAsync<string>("element => element.innerHTML");

        return new(
            null,
            new List<Target>
            {
                new("html", html),
                new("png", screenshot)
            }
        );
    }
}