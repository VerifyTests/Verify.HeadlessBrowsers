using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class SeleniumTests :
    IClassFixture<SeleniumFixture>
{
    ChromeDriver driver;

    public SeleniumTests(SeleniumFixture fixture)
    {
        driver = fixture.Driver;
    }

    [Fact]
    public async Task PageUsage()
    {
        #region SeleniumPageUsage

        await Verifier.Verify(driver);

        #endregion
    }

    [Fact]
    public async Task ElementUsage()
    {
        #region SeleniumElementUsage

        var element = driver.FindElement(By.Id("someId"));
        await Verifier.Verify(element);

        #endregion
    }
}