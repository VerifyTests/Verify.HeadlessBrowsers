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
        var size = page.ViewportSize!;
        size.Height = 768;
        size.Width = 1024;
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await page.GotoAsync("http://localhost:5000");
        await Verify(page);

        #endregion
    }

    [Test]
    public async Task ElementUsage()
    {
        #region PlaywrightElementUsage

        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://localhost:5000");
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var element = await page.QuerySelectorAsync("#someId");
        await Verify(element);

        #endregion
    }

    [OneTimeTearDown]
    public async Task DisposeAsync()
    {
        await browser.DisposeAsync();
        playwright?.Dispose();
    }
}