using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

[UsesVerify]
[Collection(SeleniumCollection.Name)]
public class SeleniumTests
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

        await Verify(driver);

        #endregion
    }

    [Fact]
    public async Task ElementUsage()
    {
        #region SeleniumElementUsage

        var element = driver.FindElement(By.Id("someId"));
        await Verify(element);

        #endregion
    }
}