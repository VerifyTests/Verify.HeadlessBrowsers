using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using VerifyTests;
using VerifyNUnit;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using WebApplication;

[TestFixture]
public class TheTests
{
    #region PageUsage

    [Test]
    public async Task PageUsage()
    {
        using var server = BuildTestServer();
        {
            var options = new FirefoxOptions();
            options.AddArgument("--headless");
            using var driver = new FirefoxDriver(options);
            driver.Manage().Window.Size = new Size(1024, 768);
            driver.Navigate().GoToUrl("http://localhost:5000");
            await Verifier.Verify(driver);
        }
    }

    #endregion

    #region ElementUsage

    //[Test]
    //public async Task ElementUsage()
    //{
    //    var element = app.WaitForElement(query => query.Marked("second"))!;
    //    await Verifier.Verify(element.Single());
    //}

    #endregion

    static TheTests()
    {
        #region Enable

        VerifySelenium.Enable();

        #endregion
        VerifyPhash.RegisterComparer("png", .99f);
    }

    #region Setup

    static async Task<IWebHost> BuildTestServer()
    {
        var webBuilder = new WebHostBuilder();

        webBuilder.UseStartup<Startup>();
        webBuilder.UseKestrel();
        var webHost = webBuilder.Build();
        await webHost.StartAsync();
        return webHost;
    }

    #endregion
}