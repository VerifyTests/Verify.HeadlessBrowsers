using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using VerifyTests;
using VerifyNUnit;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using WebApplication;

[TestFixture]
public class TheTests
{
    #region PageUsage

    [Test]
    public async Task PageUsage()
    {
        using var server = BuildTestServer();
        using var driver = new ChromeDriver
        {
            Url = server.BaseAddress.AbsoluteUri
        };
        await Verifier.Verify(driver);
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

    }

    #region Setup

    static TestServer BuildTestServer()
    {
        var webBuilder = new WebHostBuilder();

        webBuilder.UseStartup<Startup>();
        return new TestServer(webBuilder);
    }

    #endregion
}