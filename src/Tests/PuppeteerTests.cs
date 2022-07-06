using PuppeteerSharp;
using VerifyTests.Puppeteer;

public class PuppeteerTests
{
    Browser browser = null!;

    [OneTimeSetUp]
    public async Task InitializeAsync()
    {
        #region PuppeteerBuild

        // wait for target server to start
        await SocketWaiter.Wait(port: 5000);

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

    [Test]
    public async Task PageUsage()
    {
        #region PuppeteerPageUsage

        var page = await browser.NewPageAsync();
        page.Viewport.Width = 1024;
        page.Viewport.Height = 768;
        await page.GoToAsync("http://localhost:5000");
        await Verify(page);

        #endregion
    }

    [Test]
    public async Task ElementUsage()
    {
        #region PuppeteerElementUsage

        var page = await browser.NewPageAsync();
        await page.GoToAsync("http://localhost:5000");
        var element = await page.QuerySelectorAsync("#someId");
        await Verify(element);

        #endregion
    }

    [OneTimeTearDown]
    public async Task DisposeAsync()
    {
        await browser.CloseAsync();
        await browser.DisposeAsync();
    }
}