using Microsoft.Playwright;
using VerifyTests.Playwright;

public class PlaywrightTests
{
    IBrowser browser = null!;
    IPlaywright playwright = null!;

    [OneTimeSetUp]
    public async Task Initialize()
    {
        #region PlaywrightBuild

        // wait for target server to start
        await SocketWaiter.Wait(port: 5000);

        playwright = await Playwright.CreateAsync();
        browser = await playwright.Chromium.LaunchAsync();

        #endregion
    }

    [Test]
    public async Task PageUsage()
    {
        #region PlaywrightPageUsage

        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://localhost:5000");
        await Verify(page);

        #endregion
    }

    [Test]
    public async Task PageScreenshotOptions()
    {
        #region PageScreenshotOptions

        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://localhost:5000");
        await Verify(page)
            .PageScreenshotOptions(
                new()
                {
                    Quality = 50,
                    Type = ScreenshotType.Jpeg
                });

        #endregion
    }

    [Test]
    public async Task ElementUsage()
    {
        #region PlaywrightElementUsage

        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://localhost:5000");
        var element = await page.QuerySelectorAsync("#someId");
        await Verify(element!);

        #endregion
    }
    [Test]
    public async Task LocatorUsage()
    {
        #region PlaywrightLocatorUsage

        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://localhost:5000");
        var element = page.Locator("#someId");
        await Verify(element);

        #endregion
    }
    [Test]
    public async Task ElementScreenshotOptions()
    {
        #region ElementScreenshotOptions

        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://localhost:5000");
        var element = await page.QuerySelectorAsync("#someId");
        await Verify(element!)
            .ElementScreenshotOptions(
                new()
                {
                    Quality = 50,
                    Type = ScreenshotType.Jpeg
                });

        #endregion
    }

    [Test]
    public async Task LocatorScreenshotOptions()
    {
        #region LocatorScreenshotOptions

        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://localhost:5000");
        var element = page.Locator("#someId");
        await Verify(element)
            .LocatorScreenshotOptions(
                new()
                {
                    Quality = 50,
                    Type = ScreenshotType.Jpeg
                });

        #endregion
    }
    [OneTimeTearDown]
    public async Task DisposeAsync()
    {
        await browser.DisposeAsync();
        playwright?.Dispose();
    }
}