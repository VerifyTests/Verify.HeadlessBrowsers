using Microsoft.Playwright;

[UsesVerify]
[Collection(PlaywrightCollection.Name)]
public class PlaywrightTests
{
    IBrowser browser;

    public PlaywrightTests(PlaywrightFixture fixture)
    {
        browser = fixture.Browser;
    }

    [Fact]
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

    [Fact]
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
}