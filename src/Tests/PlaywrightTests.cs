using Microsoft.Playwright;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class PlaywrightTests :
    IClassFixture<PlaywrightFixture>
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
        await Verifier.Verify(page);

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
        await Verifier.Verify(element);

        #endregion
    }
}