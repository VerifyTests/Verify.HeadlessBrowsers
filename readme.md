# <img src="/src/icon.png" height="30px"> Verify.Selenium

[![Build status](https://ci.appveyor.com/api/projects/status/xbfm80k15vfqosnd?svg=true)](https://ci.appveyor.com/project/SimonCropp/verify-selenium)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.Selenium.svg)](https://www.nuget.org/packages/Verify.Selenium/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of Web UIs using [Selenium](https://www.selenium.dev/).

Support is available via a [Tidelift Subscription](https://tidelift.com/subscription/pkg/nuget-verify?utm_source=nuget-verify&utm_medium=referral&utm_campaign=enterprise).

<a href='https://dotnetfoundation.org' alt='Part of the .NET Foundation'><img src='https://raw.githubusercontent.com/VerifyTests/Verify/master/docs/dotNetFoundation.svg' height='30px'></a><br>
Part of the <a href='https://dotnetfoundation.org' alt=''>.NET Foundation</a>

<!-- toc -->
## Contents

  * [Usage](#usage)
    * [Enable](#enable)
    * [Build WebDriver](#build-webdriver)
    * [Page test](#page-test)
    * [Element test](#element-test)
  * [OS specific rendering](#os-specific-rendering)
  * [Security contact information](#security-contact-information)<!-- endToc -->


## NuGet package

https://nuget.org/packages/Verify.Selenium/


## Usage


### Enable

Enable VerifySelenium once at assembly load time:

<!-- snippet: Enable -->
<a id='snippet-enable'></a>
```cs
VerifySelenium.Enable();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L9-L13' title='Snippet source file'>snippet source</a> | <a href='#snippet-enable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Build WebDriver

<!-- snippet: BuildDriver -->
<a id='snippet-builddriver'></a>
```cs
ChromeOptions options = new();
options.AddArgument("--no-sandbox");
options.AddArgument("--headless");
Driver = new(options);
Driver.Manage().Window.Size = new(1024, 768);
Driver.Navigate().GoToUrl("http://localhost:5000");
```
<sup><a href='/src/Tests/SeleniumFixture.cs#L13-L22' title='Snippet source file'>snippet source</a> | <a href='#snippet-builddriver' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Page test

The current page state can be verified as follows:

<!-- snippet: PageUsage -->
<a id='snippet-pageusage'></a>
```cs
await Verifier.Verify(driver);
```
<sup><a href='/src/Tests/SeleniumTests.cs#L22-L26' title='Snippet source file'>snippet source</a> | <a href='#snippet-pageusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

With the state of the element being rendered as a verified files:

//snippet: TheTests.PageUsage.00.verified.html

[TheTests.PageUsage.01.verified.png](/src/Tests/TheTests.PageUsage.01.verified.png):

<img src="/src/Tests/TheTests.PageUsage.01.verified.png" width="400px">


### Element test

An element can be verified as follows:

<!-- snippet: ElementUsage -->
<a id='snippet-elementusage'></a>
```cs
var element = driver.FindElement(By.Id("someId"));
await Verifier.Verify(element);
```
<sup><a href='/src/Tests/SeleniumTests.cs#L32-L37' title='Snippet source file'>snippet source</a> | <a href='#snippet-elementusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

With the state of the element being rendered as a verified files:

//snippet: TheTests.ElementUsage.00.verified.html

[TheTests.ElementUsage.01.verified.png](/src/Tests/TheTests.ElementUsage.01.verified.png):

<img src="/src/Tests/TheTests.ElementUsage.01.verified.png">


## OS specific rendering

The rendering can very slightly between different OS versions. This can make verification on different machines (eg CI) problematic. A [custom comparer](https://github.com/VerifyTests/Verify/blob/master/docs/comparer.md) can to mitigate this.


## Security contact information

To report a security vulnerability, use the [Tidelift security contact](https://tidelift.com/security). Tidelift will coordinate the fix and disclosure.


## Icon

[Crystal](https://thenounproject.com/term/crystal/1440050/) designed by [Monjin Friends](https://thenounproject.com/monjin.friends) from [The Noun Project](https://thenounproject.com/creativepriyanka).
