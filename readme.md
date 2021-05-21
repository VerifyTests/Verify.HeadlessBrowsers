# <img src="/src/icon.png" height="30px"> Verify Headless Browsers

[![Build status](https://ci.appveyor.com/api/projects/status/n4q42hbpf32jwafg?svg=true)](https://ci.appveyor.com/project/SimonCropp/verify-headlessbrowsers)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.Playwright.svg?label=Verify.Playwright)](https://www.nuget.org/packages/Verify.Playwright/)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.Puppeteer.svg?label=Verify.Puppeteer)](https://www.nuget.org/packages/Verify.Puppeteer/)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.Selenium.svg?label=Verify.Selenium)](https://www.nuget.org/packages/Verify.Selenium/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of Web UIs using headless browsers.

<a href='https://dotnetfoundation.org' alt='Part of the .NET Foundation'><img src='https://raw.githubusercontent.com/VerifyTests/Verify/master/docs/dotNetFoundation.svg' height='30px'></a><br>
Part of the <a href='https://dotnetfoundation.org' alt=''>.NET Foundation</a>


## Playwright Usage

Verification of Web UIs via [Playwright](https://github.com/microsoft/playwright-sharp).


### NuGet package

https://nuget.org/packages/Verify.Playwright/


### Enable

Enable VerifyPlaywright once at assembly load time:

<!-- snippet: PlaywrightEnable -->
<a id='snippet-playwrightenable'></a>
```cs
VerifyPlaywright.Enable();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L16-L20' title='Snippet source file'>snippet source</a> | <a href='#snippet-playwrightenable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Instantiate browser

<!-- snippet: PlaywrightBuild -->
<a id='snippet-playwrightbuild'></a>
```cs
playwright = await Playwright.CreateAsync();
browser = await playwright.Chromium.LaunchAsync();
```
<sup><a href='/src/Tests/PlaywrightFixture.cs#L16-L21' title='Snippet source file'>snippet source</a> | <a href='#snippet-playwrightbuild' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Page test

The current page state can be verified as follows:

<!-- snippet: PlaywrightPageUsage -->
<a id='snippet-playwrightpageusage'></a>
```cs
var page = await browser.NewPageAsync();
page.ViewportSize.Height = 768;
page.ViewportSize.Width = 1024;
await page.WaitForLoadStateAsync(LifecycleEvent.Networkidle);
await page.GoToAsync("http://localhost:5000");
await Verifier.Verify(page);
```
<sup><a href='/src/Tests/PlaywrightTests.cs#L21-L30' title='Snippet source file'>snippet source</a> | <a href='#snippet-playwrightpageusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

With the state of the element being rendered as a verified files:

<!-- snippet: PlaywrightTests.PageUsage.00.verified.html -->
<a id='snippet-PlaywrightTests.PageUsage.00.verified.html'></a>
```html
<!DOCTYPE html><html lang="en"><head>
  <meta charset="utf-8">
  <title>The Title</title>
  <link href="https://getbootstrap.com/docs/4.0/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
  <div class="jumbotron">
    <h1 class="display-4">The Awareness Of Relative Idealism</h1>
    <p class="lead">
      One hears it stated that a factor within the logical radical priority embodies the
      key principles behind the best practice marginalised certification project. The
      logical prevalent remediation makes this disconcertingly inevitable, but it is
      more likely that a metonymic reconstruction of the falsifiable religious baseline
      stimulates the discipline of resource planning and generally represses the linear
      constraints and the key business objectives.
    </p>
    <a id="someId" class="btn btn-primary btn-lg" href="#" role="button">Learn more</a>
  </div>

</body></html>
```
<sup><a href='/src/Tests/PlaywrightTests.PageUsage.00.verified.html#L1-L20' title='Snippet source file'>snippet source</a> | <a href='#snippet-PlaywrightTests.PageUsage.00.verified.html' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

[PlaywrightTests.PageUsage.01.verified.png](/src/Tests/PlaywrightTests.PageUsage.01.verified.png):

<img src="/src/Tests/PlaywrightTests.PageUsage.01.verified.png" width="400px">


### Element test

An element can be verified as follows:

<!-- snippet: PlaywrightElementUsage -->
<a id='snippet-playwrightelementusage'></a>
```cs
var page = await browser.NewPageAsync();
await page.GoToAsync("http://localhost:5000");
await page.WaitForLoadStateAsync(LifecycleEvent.Networkidle);
var element = await page.QuerySelectorAsync("#someId");
await Verifier.Verify(element);
```
<sup><a href='/src/Tests/PlaywrightTests.cs#L36-L44' title='Snippet source file'>snippet source</a> | <a href='#snippet-playwrightelementusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

With the state of the element being rendered as a verified files:

<!-- snippet: PlaywrightTests.ElementUsage.00.verified.html -->
<a id='snippet-PlaywrightTests.ElementUsage.00.verified.html'></a>
```html
Learn more
```
<sup><a href='/src/Tests/PlaywrightTests.ElementUsage.00.verified.html#L1-L1' title='Snippet source file'>snippet source</a> | <a href='#snippet-PlaywrightTests.ElementUsage.00.verified.html' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

[PlaywrightTests.ElementUsage.01.verified.png](/src/Tests/PlaywrightTests.ElementUsage.01.verified.png):

<img src="/src/Tests/PlaywrightTests.ElementUsage.01.verified.png">


## Puppeteer Usage

Verification of Web UIs via [Puppeteer](https://github.com/hardkoded/puppeteer-sharp)


### NuGet package

https://nuget.org/packages/Verify.Puppeteer/


### Enable

Enable VerifyPuppeteer once at assembly load time:

<!-- snippet: PuppeteerEnable -->
<a id='snippet-puppeteerenable'></a>
```cs
VerifyPuppeteer.Enable();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L22-L26' title='Snippet source file'>snippet source</a> | <a href='#snippet-puppeteerenable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Instantiate browser

<!-- snippet: PuppeteerBuild -->
<a id='snippet-puppeteerbuild'></a>
```cs
var browserFetcher = new BrowserFetcher();
await browserFetcher.DownloadAsync();
browser = await Puppeteer.LaunchAsync(
    new LaunchOptions
    {
        Headless = true
    });
```
<sup><a href='/src/Tests/PuppeteerFixture.cs#L13-L23' title='Snippet source file'>snippet source</a> | <a href='#snippet-puppeteerbuild' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Page test

The current page state can be verified as follows:

<!-- snippet: PuppeteerPageUsage -->
<a id='snippet-puppeteerpageusage'></a>
```cs
var page = await browser.NewPageAsync();
page.Viewport.Width = 1024;
page.Viewport.Height = 768;
await page.GoToAsync("http://localhost:5000");
await Verifier.Verify(page);
```
<sup><a href='/src/Tests/PuppeteerTests.cs#L20-L28' title='Snippet source file'>snippet source</a> | <a href='#snippet-puppeteerpageusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

With the state of the element being rendered as a verified files:

<!-- snippet: PuppeteerTests.PageUsage.00.verified.html -->
<a id='snippet-PuppeteerTests.PageUsage.00.verified.html'></a>
```html
<!DOCTYPE html><html lang="en"><head>
  <meta charset="utf-8">
  <title>The Title</title>
  <link href="https://getbootstrap.com/docs/4.0/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
  <div class="jumbotron">
    <h1 class="display-4">The Awareness Of Relative Idealism</h1>
    <p class="lead">
      One hears it stated that a factor within the logical radical priority embodies the
      key principles behind the best practice marginalised certification project. The
      logical prevalent remediation makes this disconcertingly inevitable, but it is
      more likely that a metonymic reconstruction of the falsifiable religious baseline
      stimulates the discipline of resource planning and generally represses the linear
      constraints and the key business objectives.
    </p>
    <a id="someId" class="btn btn-primary btn-lg" href="#" role="button">Learn more</a>
  </div>

</body></html>
```
<sup><a href='/src/Tests/PuppeteerTests.PageUsage.00.verified.html#L1-L20' title='Snippet source file'>snippet source</a> | <a href='#snippet-PuppeteerTests.PageUsage.00.verified.html' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

[PuppeteerTests.PageUsage.01.verified.png](/src/Tests/PuppeteerTests.PageUsage.01.verified.png):

<img src="/src/Tests/PuppeteerTests.PageUsage.01.verified.png" width="400px">


### Element test

An element can be verified as follows:

<!-- snippet: PuppeteerElementUsage -->
<a id='snippet-puppeteerelementusage'></a>
```cs
var page = await browser.NewPageAsync();
await page.GoToAsync("http://localhost:5000");
var element = await page.QuerySelectorAsync("#someId");
await Verifier.Verify(element);
```
<sup><a href='/src/Tests/PuppeteerTests.cs#L34-L41' title='Snippet source file'>snippet source</a> | <a href='#snippet-puppeteerelementusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

With the state of the element being rendered as a verified files:

<!-- snippet: PuppeteerTests.ElementUsage.00.verified.html -->
<a id='snippet-PuppeteerTests.ElementUsage.00.verified.html'></a>
```html
Learn more
```
<sup><a href='/src/Tests/PuppeteerTests.ElementUsage.00.verified.html#L1-L1' title='Snippet source file'>snippet source</a> | <a href='#snippet-PuppeteerTests.ElementUsage.00.verified.html' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

[PuppeteerTests.ElementUsage.01.verified.png](/src/Tests/PuppeteerTests.ElementUsage.01.verified.png):

<img src="/src/Tests/PuppeteerTests.ElementUsage.01.verified.png">


## Selenium Usage

Verification of Web UIs via [Selenium](https://www.selenium.dev).


### NuGet package

https://nuget.org/packages/Verify.Selenium/


### Enable

Enable VerifySelenium once at assembly load time:

<!-- snippet: SeleniumEnable -->
<a id='snippet-seleniumenable'></a>
```cs
VerifySelenium.Enable();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L10-L14' title='Snippet source file'>snippet source</a> | <a href='#snippet-seleniumenable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Instantiate browser

<!-- snippet: SeleniumBuildDriver -->
<a id='snippet-seleniumbuilddriver'></a>
```cs
ChromeOptions options = new();
options.AddArgument("--no-sandbox");
options.AddArgument("--headless");
Driver = new(options);
Driver.Manage().Window.Size = new(1024, 768);
Driver.Navigate().GoToUrl("http://localhost:5000");
```
<sup><a href='/src/Tests/SeleniumFixture.cs#L12-L21' title='Snippet source file'>snippet source</a> | <a href='#snippet-seleniumbuilddriver' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Page test

The current page state can be verified as follows:

<!-- snippet: SeleniumPageUsage -->
<a id='snippet-seleniumpageusage'></a>
```cs
await Verifier.Verify(driver);
```
<sup><a href='/src/Tests/SeleniumTests.cs#L21-L25' title='Snippet source file'>snippet source</a> | <a href='#snippet-seleniumpageusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

With the state of the element being rendered as a verified files:

<!-- snippet: SeleniumTests.PageUsage.00.verified.html -->
<a id='snippet-SeleniumTests.PageUsage.00.verified.html'></a>
```html
<html lang="en"><head>
  <meta charset="utf-8">
  <title>The Title</title>
  <link href="https://getbootstrap.com/docs/4.0/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
  <div class="jumbotron">
    <h1 class="display-4">The Awareness Of Relative Idealism</h1>
    <p class="lead">
      One hears it stated that a factor within the logical radical priority embodies the
      key principles behind the best practice marginalised certification project. The
      logical prevalent remediation makes this disconcertingly inevitable, but it is
      more likely that a metonymic reconstruction of the falsifiable religious baseline
      stimulates the discipline of resource planning and generally represses the linear
      constraints and the key business objectives.
    </p>
    <a id="someId" class="btn btn-primary btn-lg" href="#" role="button">Learn more</a>
  </div>

</body></html>
```
<sup><a href='/src/Tests/SeleniumTests.PageUsage.00.verified.html#L1-L20' title='Snippet source file'>snippet source</a> | <a href='#snippet-SeleniumTests.PageUsage.00.verified.html' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

[SeleniumTests.PageUsage.01.verified.png](/src/Tests/SeleniumTests.PageUsage.01.verified.png):

<img src="/src/Tests/SeleniumTests.PageUsage.01.verified.png" width="400px">


### Element test

An element can be verified as follows:

<!-- snippet: SeleniumElementUsage -->
<a id='snippet-seleniumelementusage'></a>
```cs
var element = driver.FindElement(By.Id("someId"));
await Verifier.Verify(element);
```
<sup><a href='/src/Tests/SeleniumTests.cs#L31-L36' title='Snippet source file'>snippet source</a> | <a href='#snippet-seleniumelementusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

With the state of the element being rendered as a verified files:

<!-- snippet: SeleniumTests.ElementUsage.00.verified.html -->
<a id='snippet-SeleniumTests.ElementUsage.00.verified.html'></a>
```html
<a id="someId" class="btn btn-primary btn-lg" href="#" role="button">Learn more</a>
```
<sup><a href='/src/Tests/SeleniumTests.ElementUsage.00.verified.html#L1-L1' title='Snippet source file'>snippet source</a> | <a href='#snippet-SeleniumTests.ElementUsage.00.verified.html' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

[SeleniumTests.ElementUsage.01.verified.png](/src/Tests/SeleniumTests.ElementUsage.01.verified.png):

<img src="/src/Tests/SeleniumTests.ElementUsage.01.verified.png">


## OS specific rendering

The rendering can very slightly between different OS versions. This can make verification on different machines (eg CI) problematic. A [custom comparer](https://github.com/VerifyTests/Verify/blob/master/docs/comparer.md) can to mitigate this.



## Icon

[Crystal](https://thenounproject.com/term/crystal/1440050/) designed by [Monjin Friends](https://thenounproject.com/monjin.friends) from [The Noun Project](https://thenounproject.com/creativepriyanka).
