using Microsoft.Playwright;

public class PlaywrightFixture :
    IAsyncLifetime
{
    IPlaywright? playwright;
    IBrowser? browser;

    public IBrowser Browser { get => browser!; }

    public async Task InitializeAsync()
    {
        #region PlaywrightBuild

        playwright = await Playwright.CreateAsync();
        browser = await playwright.Chromium.LaunchAsync();

        #endregion
    }

    public async Task DisposeAsync()
    {
        if (browser != null)
        {
            await browser.DisposeAsync();
        }

        playwright?.Dispose();
    }
}