using PuppeteerSharp;

public class PuppeteerFixture :
    IAsyncLifetime
{
    Browser? browser;
    public Browser Browser { get => browser!; }

    public async Task InitializeAsync()
    {
        #region PuppeteerBuild

        var browserFetcher = new BrowserFetcher(Product.Chrome);
        await browserFetcher.DownloadAsync();
        browser = await Puppeteer.LaunchAsync(
            new()
            {
                Headless = true
            });

        #endregion
    }

    public async Task DisposeAsync()
    {
        if (browser != null)
        {
            await browser.CloseAsync();
            await browser.DisposeAsync();
        }
    }
}