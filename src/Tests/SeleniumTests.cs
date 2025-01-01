#if DEBUG
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using VerifyTests.Selenium;

public class SeleniumTests
{
    ChromeDriver driver = null!;

    [OneTimeSetUp]
    public async Task InitializeAsync()
    {
        #region SeleniumBuildDriver

        // wait for target server to start
        await SocketWaiter.Wait(port: 5000);

        var options = new ChromeOptions();
        options.AddArgument("--no-sandbox");
        options.AddArgument("--headless");
        driver = new(options);
        driver.Manage().Window.Size = new(1024, 768);
        driver.Navigate().GoToUrl("http://localhost:5000");

        #endregion
    }

    [Test]
    public async Task PageUsage()
    {
        #region SeleniumPageUsage

        await Verify(driver);

        #endregion
    }

    [Test]
    public async Task ElementUsage()
    {
        #region SeleniumElementUsage

        var element = driver.FindElement(By.Id("someId"));
        await Verify(element);

        #endregion
    }

    [OneTimeTearDown]
    public void Dispose()
    {
        driver.Quit();
        driver.Dispose();
    }
}
#endif