using PuppeteerSharp;

[UsesVerify]
public class PuppeteerTests :
    IClassFixture<PuppeteerFixture>
{
    Browser browser;

    public PuppeteerTests(PuppeteerFixture fixture)
    {
        browser = fixture.Browser;
    }

    [Fact]
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

    [Fact]
    public async Task ElementUsage()
    {
        #region PuppeteerElementUsage

        var page = await browser.NewPageAsync();
        await page.GoToAsync("http://localhost:5000");
        var element = await page.QuerySelectorAsync("#someId");
        await Verify(element);

        #endregion
    }
}