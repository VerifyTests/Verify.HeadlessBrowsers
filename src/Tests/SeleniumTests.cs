using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class SeleniumTests :
    IClassFixture<WebStartupFixture>,
    IClassFixture<SeleniumFixture>
{
    ChromeDriver driver;

    public SeleniumTests(SeleniumFixture seleniumFixture)
    {
        driver = seleniumFixture.Driver;
    }

    [Fact]
    public async Task PageUsage()
    {
        #region PageUsage

        await Verifier.Verify(driver);

        #endregion
    }

    [Fact]
    public async Task ElementUsage()
    {
        #region ElementUsage

        var element = driver.FindElement(By.Id("someId"));
        await Verifier.Verify(element);

        #endregion
    }
}