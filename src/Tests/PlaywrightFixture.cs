using System.Threading.Tasks;
using PlaywrightSharp;
using PlaywrightSharp.Chromium;
using Xunit;

public class PlaywrightFixture :
    IAsyncLifetime
{
    IPlaywright? playwright;
    IChromiumBrowser? browser;

    public IChromiumBrowser Browser { get => browser!; }

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