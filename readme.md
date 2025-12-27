# <img src="/src/icon.png" height="30px"> Verify Headless Browsers

[![Discussions](https://img.shields.io/badge/Verify-Discussions-yellow?svg=true&label=)](https://github.com/orgs/VerifyTests/discussions)
[![Build status](https://img.shields.io/appveyor/build/SimonCropp/verify-headlessbrowsers)](https://ci.appveyor.com/project/SimonCropp/verify-headlessbrowsers)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.Playwright.svg?label=Verify.Playwright)](https://www.nuget.org/packages/Verify.Playwright/)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.Puppeteer.svg?label=Verify.Puppeteer)](https://www.nuget.org/packages/Verify.Puppeteer/)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.Selenium.svg?label=Verify.Selenium)](https://www.nuget.org/packages/Verify.Selenium/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of Web UIs using headless browsers.<!-- singleLineInclude: intro. path: /docs/intro.include.md -->

**See [Milestones](../../milestones?state=closed) for release notes.**


## Sponsors


### Entity Framework Extensions<!-- include: zzz. path: /docs/zzz.include.md -->

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.HeadlessBrowsers) is a major sponsor and is proud to contribute to the development this project.

[![Entity Framework Extensions](https://raw.githubusercontent.com/VerifyTests/Verify.HeadlessBrowsers/refs/heads/main/docs/zzz.png)](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.HeadlessBrowsers)<!-- endInclude -->


## NuGet

 * https://nuget.org/packages/Verify.Playwright
 * https://nuget.org/packages/Verify.Puppeteer
 * https://nuget.org/packages/Verify.Selenium


## Playwright Usage

Verification of Web UIs via [Playwright](https://github.com/microsoft/playwright-sharp).


### Enable

Enable VerifyPlaywright once at assembly load time:

<!-- snippet: PlaywrightEnable -->
<a id='snippet-PlaywrightEnable'></a>
```cs
[ModuleInitializer]
public static void InitPlaywright() =>
    VerifyPlaywright.Initialize(installPlaywright: true);
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L11-L17' title='Snippet source file'>snippet source</a> | <a href='#snippet-PlaywrightEnable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Instantiate browser

<!-- snippet: PlaywrightBuild -->
<a id='snippet-PlaywrightBuild'></a>
```cs
// wait for target server to start
await SocketWaiter.Wait(port: 5000);

playwright = await Playwright.CreateAsync();
browser = await playwright.Chromium.LaunchAsync();
```
<sup><a href='/src/Tests/PlaywrightTests.cs#L13-L21' title='Snippet source file'>snippet source</a> | <a href='#snippet-PlaywrightBuild' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Page test

The current page state can be verified as follows:

<!-- snippet: PlaywrightPageUsage -->
<a id='snippet-PlaywrightPageUsage'></a>
```cs
var page = await browser.NewPageAsync();
await page.GotoAsync("http://localhost:5000");
await Verify(page);
```
<sup><a href='/src/Tests/PlaywrightTests.cs#L27-L33' title='Snippet source file'>snippet source</a> | <a href='#snippet-PlaywrightPageUsage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

With the state of the element being rendered as a verified files:

<!-- snippet: PlaywrightTests.PageUsage.verified.html -->
<a id='snippet-PlaywrightTests.PageUsage.verified.html'></a>
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
<sup><a href='/src/Tests/PlaywrightTests.PageUsage.verified.html#L1-L20' title='Snippet source file'>snippet source</a> | <a href='#snippet-PlaywrightTests.PageUsage.verified.html' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

[PlaywrightTests.PageUsage.01.verified.png](/src/Tests/PlaywrightTests.PageUsage.verified.png):

<img src="/src/Tests/PlaywrightTests.PageUsage.verified.png" width="400px">


#### PageScreenshotOptions

<!-- snippet: PageScreenshotOptions -->
<a id='snippet-PageScreenshotOptions'></a>
```cs
var page = await browser.NewPageAsync();
await page.GotoAsync("http://localhost:5000");
await Verify(page)
    .PageScreenshotOptions(
        new()
        {
            Quality = 50,
            Type = ScreenshotType.Jpeg
        });
```
<sup><a href='/src/Tests/PlaywrightTests.cs#L52-L64' title='Snippet source file'>snippet source</a> | <a href='#snippet-PageScreenshotOptions' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


#### Page screenshot only option

Passing true to `screenshotOnly` will avoid producing HTML, instead only producing the Page image

<!-- snippet: PageScreenshotOnlyOptions -->
<a id='snippet-PageScreenshotOnlyOptions'></a>
```cs
var page = await browser.NewPageAsync();
await page.GotoAsync("http://localhost:5000");
await Verify(page)
    .PageScreenshotOptions(
        new()
        {
            Quality = 50,
            Type = ScreenshotType.Jpeg
        },
        screenshotOnly: true);
```
<sup><a href='/src/Tests/PlaywrightTests.cs#L70-L83' title='Snippet source file'>snippet source</a> | <a href='#snippet-PageScreenshotOnlyOptions' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Element test

An element can be verified as follows:

<!-- snippet: PlaywrightElementUsage -->
<a id='snippet-PlaywrightElementUsage'></a>
```cs
var page = await browser.NewPageAsync();
await page.GotoAsync("http://localhost:5000");
var element = await page.QuerySelectorAsync("#someId");
await Verify(element!);
```
<sup><a href='/src/Tests/PlaywrightTests.cs#L89-L96' title='Snippet source file'>snippet source</a> | <a href='#snippet-PlaywrightElementUsage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

With the state of the element being rendered as a verified files:

<!-- snippet: PlaywrightTests.ElementUsage.verified.html -->
<a id='snippet-PlaywrightTests.ElementUsage.verified.html'></a>
```html
Learn more
```
<sup><a href='/src/Tests/PlaywrightTests.ElementUsage.verified.html#L1-L1' title='Snippet source file'>snippet source</a> | <a href='#snippet-PlaywrightTests.ElementUsage.verified.html' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

[PlaywrightTests.ElementUsage.01.verified.png](/src/Tests/PlaywrightTests.ElementUsage.verified.png):

<img src="/src/Tests/PlaywrightTests.ElementUsage.verified.png">


#### ElementScreenshotOptions

<!-- snippet: ElementScreenshotOptions -->
<a id='snippet-ElementScreenshotOptions'></a>
```cs
var page = await browser.NewPageAsync();
await page.GotoAsync("http://localhost:5000");
var element = await page.QuerySelectorAsync("#someId");
await Verify(element!)
    .ElementScreenshotOptions(
        new()
        {
            Quality = 50,
            Type = ScreenshotType.Jpeg
        });
```
<sup><a href='/src/Tests/PlaywrightTests.cs#L115-L128' title='Snippet source file'>snippet source</a> | <a href='#snippet-ElementScreenshotOptions' title='Start of snippet'>anchor</a></sup>
<a id='snippet-ElementScreenshotOptions-1'></a>
```cs
var page = await browser.NewPageAsync();
await page.GotoAsync("http://localhost:5000");
var element = await page.QuerySelectorAsync("#someId");
await Verify(element!)
    .ElementScreenshotOptions(
        new()
        {
            Quality = 50,
            Type = ScreenshotType.Jpeg
        },
        screenshotOnly: true);
```
<sup><a href='/src/Tests/PlaywrightTests.cs#L134-L148' title='Snippet source file'>snippet source</a> | <a href='#snippet-ElementScreenshotOptions-1' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Element test using ILocator

An element can be verified as follows:

<!-- snippet: PlaywrightLocatorUsage -->
<a id='snippet-PlaywrightLocatorUsage'></a>
```cs
var page = await browser.NewPageAsync();
await page.GotoAsync("http://localhost:5000");
var element = page.Locator("#someId");
await Verify(element);
```
<sup><a href='/src/Tests/PlaywrightTests.cs#L102-L109' title='Snippet source file'>snippet source</a> | <a href='#snippet-PlaywrightLocatorUsage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

With the state of the element being rendered as a verified files:

<!-- snippet: PlaywrightTests.LocatorUsage.verified.html -->
<a id='snippet-PlaywrightTests.LocatorUsage.verified.html'></a>
```html
Learn more
```
<sup><a href='/src/Tests/PlaywrightTests.LocatorUsage.verified.html#L1-L1' title='Snippet source file'>snippet source</a> | <a href='#snippet-PlaywrightTests.LocatorUsage.verified.html' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

[PlaywrightTests.LocatorUsage.01.verified.png](/src/Tests/PlaywrightTests.LocatorUsage.verified.png):

<img src="/src/Tests/PlaywrightTests.LocatorUsage.verified.png">


#### LocatorScreenshotOptions

<!-- snippet: LocatorScreenshotOptions -->
<a id='snippet-LocatorScreenshotOptions'></a>
```cs
var page = await browser.NewPageAsync();
await page.GotoAsync("http://localhost:5000");
var element = page.Locator("#someId");
await Verify(element)
    .LocatorScreenshotOptions(
        new()
        {
            Quality = 50,
            Type = ScreenshotType.Jpeg
        });
```
<sup><a href='/src/Tests/PlaywrightTests.cs#L154-L167' title='Snippet source file'>snippet source</a> | <a href='#snippet-LocatorScreenshotOptions' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


#### Locator screenshot only option

Passing true to `screenshotOnly` will avoid producing HTML, instead only producing the Locator image

<!-- snippet: LocatorScreenshotOnlyOptions -->
<a id='snippet-LocatorScreenshotOnlyOptions'></a>
```cs
var page = await browser.NewPageAsync();
await page.GotoAsync("http://localhost:5000");
var element = page.Locator("#someId");
await Verify(element)
    .LocatorScreenshotOptions(
        new()
        {
            Quality = 50,
            Type = ScreenshotType.Jpeg
        },
        screenshotOnly: true);
```
<sup><a href='/src/Tests/PlaywrightTests.cs#L173-L187' title='Snippet source file'>snippet source</a> | <a href='#snippet-LocatorScreenshotOnlyOptions' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Puppeteer Usage

Verification of Web UIs via [Puppeteer](https://github.com/hardkoded/puppeteer-sharp)


### Enable

Enable VerifyPuppeteer once at assembly load time:

<!-- snippet: PuppeteerEnable -->
<a id='snippet-PuppeteerEnable'></a>
```cs
[ModuleInitializer]
public static void InitPuppeteer() =>
    VerifyPuppeteer.Initialize();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L19-L25' title='Snippet source file'>snippet source</a> | <a href='#snippet-PuppeteerEnable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Instantiate browser

<!-- snippet: PuppeteerBuild -->
<a id='snippet-PuppeteerBuild'></a>
```cs
// wait for target server to start
await SocketWaiter.Wait(port: 5000);

var fetcher = new BrowserFetcher(SupportedBrowser.Chrome);
await fetcher.DownloadAsync();

browser = await Puppeteer.LaunchAsync(
    new()
    {
        Headless = true
    });
```
<sup><a href='/src/Tests/PuppeteerTests.cs#L11-L25' title='Snippet source file'>snippet source</a> | <a href='#snippet-PuppeteerBuild' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Page test

The current page state can be verified as follows:

<!-- snippet: PuppeteerPageUsage -->
<a id='snippet-PuppeteerPageUsage'></a>
```cs
var page = await browser.NewPageAsync();
page.Viewport.Width = 1024;
page.Viewport.Height = 768;
await page.GoToAsync("http://localhost:5000");
await Verify(page);
```
<sup><a href='/src/Tests/PuppeteerTests.cs#L31-L39' title='Snippet source file'>snippet source</a> | <a href='#snippet-PuppeteerPageUsage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

With the state of the element being rendered as a verified files:

<!-- snippet: PuppeteerTests.PageUsage.verified.html -->
<a id='snippet-PuppeteerTests.PageUsage.verified.html'></a>
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
<sup><a href='/src/Tests/PuppeteerTests.PageUsage.verified.html#L1-L20' title='Snippet source file'>snippet source</a> | <a href='#snippet-PuppeteerTests.PageUsage.verified.html' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

[PuppeteerTests.PageUsage.01.verified.png](/src/Tests/PuppeteerTests.PageUsage.verified.png):

<img src="/src/Tests/PuppeteerTests.PageUsage.verified.png" width="400px">


### Element test

An element can be verified as follows:

<!-- snippet: PuppeteerElementUsage -->
<a id='snippet-PuppeteerElementUsage'></a>
```cs
var page = await browser.NewPageAsync();
await page.GoToAsync("http://localhost:5000");
var element = await page.QuerySelectorAsync("#someId");
await Verify(element);
```
<sup><a href='/src/Tests/PuppeteerTests.cs#L45-L52' title='Snippet source file'>snippet source</a> | <a href='#snippet-PuppeteerElementUsage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

With the state of the element being rendered as a verified files:

<!-- snippet: PuppeteerTests.ElementUsage.verified.html -->
<a id='snippet-PuppeteerTests.ElementUsage.verified.html'></a>
```html
Learn more
```
<sup><a href='/src/Tests/PuppeteerTests.ElementUsage.verified.html#L1-L1' title='Snippet source file'>snippet source</a> | <a href='#snippet-PuppeteerTests.ElementUsage.verified.html' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

[PuppeteerTests.ElementUsage.01.verified.png](/src/Tests/PuppeteerTests.ElementUsage.verified.png):

<img src="/src/Tests/PuppeteerTests.ElementUsage.verified.png">


## Selenium Usage

Verification of Web UIs via [Selenium](https://www.selenium.dev).


### Enable

Enable VerifySelenium once at assembly load time:

<!-- snippet: SeleniumEnable -->
<a id='snippet-SeleniumEnable'></a>
```cs
[ModuleInitializer]
public static void InitSelenium() =>
    VerifySelenium.Initialize();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-SeleniumEnable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Instantiate browser

<!-- snippet: SeleniumBuildDriver -->
<a id='snippet-SeleniumBuildDriver'></a>
```cs
// wait for target server to start
await SocketWaiter.Wait(port: 5000);

var options = new ChromeOptions();
options.AddArgument("--no-sandbox");
options.AddArgument("--headless");
driver = new(options);
driver.Manage().Window.Size = new(1024, 768);
await driver.Navigate().GoToUrlAsync("http://localhost:5000");
```
<sup><a href='/src/Tests/SeleniumTests.cs#L14-L26' title='Snippet source file'>snippet source</a> | <a href='#snippet-SeleniumBuildDriver' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Page test

The current page state can be verified as follows:

<!-- snippet: SeleniumPageUsage -->
<a id='snippet-SeleniumPageUsage'></a>
```cs
await Verify(driver);
```
<sup><a href='/src/Tests/SeleniumTests.cs#L33-L37' title='Snippet source file'>snippet source</a> | <a href='#snippet-SeleniumPageUsage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

With the state of the element being rendered as a verified files:

<!-- snippet: SeleniumTests.PageUsage.verified.html -->
<a id='snippet-SeleniumTests.PageUsage.verified.html'></a>
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
<sup><a href='/src/Tests/SeleniumTests.PageUsage.verified.html#L1-L20' title='Snippet source file'>snippet source</a> | <a href='#snippet-SeleniumTests.PageUsage.verified.html' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

[SeleniumTests.PageUsage.01.verified.png](/src/Tests/SeleniumTests.PageUsage.verified.png):

<img src="/src/Tests/SeleniumTests.PageUsage.verified.png" width="400px">


### Element test

An element can be verified as follows:

<!-- snippet: SeleniumElementUsage -->
<a id='snippet-SeleniumElementUsage'></a>
```cs
var element = driver.FindElement(By.Id("someId"));
await Verify(element);
```
<sup><a href='/src/Tests/SeleniumTests.cs#L43-L48' title='Snippet source file'>snippet source</a> | <a href='#snippet-SeleniumElementUsage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

With the state of the element being rendered as a verified files:

<!-- snippet: SeleniumTests.ElementUsage.verified.html -->
<a id='snippet-SeleniumTests.ElementUsage.verified.html'></a>
```html
<a id="someId" class="btn btn-primary btn-lg" href="#" role="button">Learn more</a>
```
<sup><a href='/src/Tests/SeleniumTests.ElementUsage.verified.html#L1-L1' title='Snippet source file'>snippet source</a> | <a href='#snippet-SeleniumTests.ElementUsage.verified.html' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

[SeleniumTests.ElementUsage.01.verified.png](/src/Tests/SeleniumTests.ElementUsage.verified.png):

<img src="/src/Tests/SeleniumTests.ElementUsage.verified.png">


## OS specific rendering

The rendering can very slightly between different OS versions. This can make verification on different machines (eg CI) problematic. A [custom comparer](https://github.com/VerifyTests/Verify/blob/master/docs/comparer.md) can to mitigate this.


## Compatibility with other Verify plugins

This projects is designed to be compatible with other Verify plugins. One common scenario for combining plugins is using [Verify.AngleSharp](https://github.com/VerifyTests/Verify.AngleSharp) to manipulate the verified html.

For example using the Verify.AngleSharp [Pretty Print](https://github.com/VerifyTests/Verify.AngleSharp?tab=readme-ov-file#pretty-print) extension.

<!-- snippet: UsageWithAngleSharp -->
<a id='snippet-UsageWithAngleSharp'></a>
```cs
var page = await browser.NewPageAsync();
await page.GotoAsync("http://localhost:5000");
await Verify(page)
    .PrettyPrintHtml();
```
<sup><a href='/src/Tests/PlaywrightTests.cs#L39-L46' title='Snippet source file'>snippet source</a> | <a href='#snippet-UsageWithAngleSharp' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Without the `.PrettyPrintHtml()` the result is:

<!-- snippet: PlaywrightTests.PageUsage.verified.html -->
<a id='snippet-PlaywrightTests.PageUsage.verified.html'></a>
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
<sup><a href='/src/Tests/PlaywrightTests.PageUsage.verified.html#L1-L20' title='Snippet source file'>snippet source</a> | <a href='#snippet-PlaywrightTests.PageUsage.verified.html' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

but with it added, the result is:

<!-- snippet: PlaywrightTests.UsageWithAngleSharp.verified.html -->
<a id='snippet-PlaywrightTests.UsageWithAngleSharp.verified.html'></a>
```html
<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>The Title</title>
    <link href="https://getbootstrap.com/docs/4.0/dist/css/bootstrap.min.css" rel="stylesheet">
  </head>
  <body>
    <div class="jumbotron">
      <h1 class="display-4">The Awareness Of Relative Idealism</h1>
      <p class="lead">
        One hears it stated that a factor within the logical radical priority embodies the       key principles behind the best practice marginalised certification project. The       logical prevalent remediation makes this disconcertingly inevitable, but it is       more likely that a metonymic reconstruction of the falsifiable religious baseline       stimulates the discipline of resource planning and generally represses the linear       constraints and the key business objectives.
      </p>
      <a id="someId" class="btn btn-primary btn-lg" href="#" role="button">Learn more</a>
    </div>
  </body>
</html>
```
<sup><a href='/src/Tests/PlaywrightTests.UsageWithAngleSharp.verified.html#L1-L17' title='Snippet source file'>snippet source</a> | <a href='#snippet-PlaywrightTests.UsageWithAngleSharp.verified.html' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

## Icon

[Crystal](https://thenounproject.com/term/crystal/1440050/) designed by [Monjin Friends](https://thenounproject.com/monjin.friends) from [The Noun Project](https://thenounproject.com).
