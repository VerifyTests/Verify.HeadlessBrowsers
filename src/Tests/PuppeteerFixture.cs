using PuppeteerSharp;

[CollectionDefinition(Name)]
public sealed class PuppeteerCollection :
    ICollectionFixture<PuppeteerFixture>
{
    public const string Name = "Puppeteer";
}

public class PuppeteerFixture :
    IAsyncLifetime
{
    Browser? browser;
    public Browser Browser { get => browser!; }

    public async Task InitializeAsync()
    {
        #region PuppeteerBuild

        using (var fetcher = new BrowserFetcher(Product.Chrome))
        {
            await fetcher.DownloadAsync();
        }

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