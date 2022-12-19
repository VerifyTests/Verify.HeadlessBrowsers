using System.Diagnostics.CodeAnalysis;
using Microsoft.Playwright;

namespace VerifyTests;

public static class VerifyPlaywright
{
    public static void Enable(bool installPlaywright = false)
    {
        InnerVerifier.ThrowIfVerifyHasBeenRun();
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

        Task<byte[]> bytes;
        var imageType = "png";
        if (context.GetPageScreenshotOptions(out var options))
        {
            bytes = page.ScreenshotAsync(options);
            if (options.Type == ScreenshotType.Jpeg)
            {
                imageType = "jpg";
            }
        }
        else
        {
            bytes = page.ScreenshotAsync(
                new()
                {
                    FullPage = true,
                    Type = ScreenshotType.Png
                });
        }

        await RemovePlaywrightStyle(page);
        var html = await page.ContentAsync();
        return new(
            null,
            new List<Target>
            {
                new("html", html),
                new(imageType, new MemoryStream(await bytes))
            }
        );
    }

    static async Task RemovePlaywrightStyle(IPage page)
    {
        var elements = await page.QuerySelectorAllAsync("style");
        foreach (var element in elements)
        {
            var value = await element.InnerHTMLAsync();
            if (value.Contains("*:not(#playwright"))
            {
                await element.EvaluateAsync("element => element.remove()", element);
            }
        }
    }

    static async Task<ConversionResult> ElementToImage(IElementHandle element, IReadOnlyDictionary<string, object> context)
    {
        Task<byte[]> bytes;
        var imageType = "png";
        if (context.GetElementScreenshotOptions(out var options))
        {
            bytes = element.ScreenshotAsync(options);
            if (options.Type == ScreenshotType.Jpeg)
            {
                imageType = "jpg";
            }
        }
        else
        {
            bytes = element.ScreenshotAsync(
                new()
                {
                    Type = ScreenshotType.Png
                });
        }

        var html = element.InnerHTMLAsync();
        return new(
            null,
            new List<Target>
            {
                new("html", await html),
                new(imageType, new MemoryStream(await bytes))
            }
        );
    }

    public static void PageScreenshotOptions(this VerifySettings settings, PageScreenshotOptions options) =>
        settings.Context["Playwright.PageScreenshotOptions"] = options;

    public static SettingsTask PageScreenshotOptions(this SettingsTask settings, PageScreenshotOptions options)
    {
        settings.CurrentSettings.PageScreenshotOptions(options);
        return settings;
    }

    static bool GetPageScreenshotOptions(this IReadOnlyDictionary<string, object> context, [NotNullWhen(true)] out PageScreenshotOptions? options)
    {
        if (context.TryGetValue("Playwright.PageScreenshotOptions", out var value))
        {
            options = (PageScreenshotOptions) value;
            ValidateNoPath(options.Path);
            return true;
        }

        options = null;
        return false;
    }

    static void ValidateNoPath(string? path)
    {
        if (path != null)
        {
            throw new("ScreenshotOptions Path not supported.");
        }
    }

    public static void ElementScreenshotOptions(this VerifySettings settings, ElementHandleScreenshotOptions options) =>
        settings.Context["Playwright.ElementScreenshotOptions"] = options;

    public static SettingsTask ElementScreenshotOptions(this SettingsTask settings, ElementHandleScreenshotOptions options)
    {
        settings.CurrentSettings.ElementScreenshotOptions(options);
        return settings;
    }

    static bool GetElementScreenshotOptions(this IReadOnlyDictionary<string, object> context, [NotNullWhen(true)] out ElementHandleScreenshotOptions? options)
    {
        if (context.TryGetValue("Playwright.ElementScreenshotOptions", out var value))
        {
            options = (ElementHandleScreenshotOptions) value;
            ValidateNoPath(options.Path);
            return true;
        }

        options = null;
        return false;
    }

}