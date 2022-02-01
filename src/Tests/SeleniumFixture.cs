using OpenQA.Selenium.Chrome;

[CollectionDefinition(Name)]
public sealed class SeleniumCollection :
    ICollectionFixture<SeleniumFixture>
{
    public const string Name = "Selenium";
}

public class SeleniumFixture :
    IAsyncLifetime
{
    public ChromeDriver Driver { get; private set; } = null!;

    public Task InitializeAsync()
    {
        #region SeleniumBuildDriver

        var options = new ChromeOptions();
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