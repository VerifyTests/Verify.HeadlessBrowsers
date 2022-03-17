﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class SeleniumTests :
    IDisposable
{
    ChromeDriver driver;

    public SeleniumTests()
    {
        #region SeleniumBuildDriver

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

    public void Dispose()
    {
        driver.Quit();
        driver.Dispose();
    }
}