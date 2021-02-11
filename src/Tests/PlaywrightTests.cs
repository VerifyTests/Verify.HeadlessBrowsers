using System.Threading.Tasks;
using PlaywrightSharp;
using PlaywrightSharp.Chromium;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class PlaywrightTests :
    IClassFixture<PlaywrightFixture>
{
    IChromiumBrowser browser;

    public PlaywrightTests(PlaywrightFixture fixture)
    {
        browser = fixture.Browser;
    }

    [Fact]
    public async Task PageUsage()
    {
        #region PlaywrightPageUsage

        var page = await browser.NewPageAsync();
        await page.GoToAsync("http://localhost:5000");
        await Verifier.Verify(page);

        #endregion
    }

    [Fact]
    public async Task ElementUsage()
    {
        #region PlaywrightElementUsage

        var page = await browser.NewPageAsync();
        await page.GoToAsync("http://localhost:5000");
        await page.WaitForLoadStateAsync(LifecycleEvent.Networkidle);
        var element = await page.QuerySelectorAsync("#someId");
        await Verifier.Verify(element);

        #endregion
    }
}