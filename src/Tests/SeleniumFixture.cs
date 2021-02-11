using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using Xunit;

public class SeleniumFixture :
    IAsyncLifetime
{
    public ChromeDriver Driver { get; private set; } = null!;

    public Task InitializeAsync()
    {
        #region SeleniumBuildDriver

        ChromeOptions options = new();
        options.AddArgument("--no-sandbox");
        options.AddArgument("--headless");
        Driver = new(options);
        Driver.Manage().Window.Size = new(1024, 768);
        Driver.Navigate().GoToUrl("http://localhost:5000");

        #endregion

        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        Driver.Quit();
        Driver.Dispose();
        return Task.CompletedTask;
    }
}