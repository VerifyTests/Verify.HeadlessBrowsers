# <img src="/src/icon.png" height="30px"> Verify.Selenium

[![Build status](https://ci.appveyor.com/api/projects/status/xbfm80k15vfqosnd?svg=true)](https://ci.appveyor.com/project/SimonCropp/verify-selenium)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.Selenium.svg)](https://www.nuget.org/packages/Verify.Selenium/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of Web UIs using [Selenium](https://www.selenium.dev/).


Support is available via a [Tidelift Subscription](https://tidelift.com/subscription/pkg/nuget-verify.selenium?utm_source=nuget-verify.selenium&utm_medium=referral&utm_campaign=enterprise).


toc


## NuGet package

https://nuget.org/packages/Verify.Selenium/


## Usage


### Enable

Enable VerifySelenium once at assembly load time:


### Build WebDriver

snippet: BuildDriver


### Page test

The current page state can be verified as follows:

snippet: PageUsage

With the state of the element being rendered as a verified files:

snippet: TheTests.PageUsage.00.verified.html

[TheTests.PageUsage.01.verified.png](/src/Tests/TheTests.PageUsage.01.verified.png):

<img src="/src/Tests/TheTests.PageUsage.01.verified.png" width="400px">


### Element test

An element can be verified as follows:

snippet: ElementUsage

With the state of the element being rendered as a verified files:

snippet: TheTests.ElementUsage.00.verified.html

[TheTests.ElementUsage.01.verified.png](/src/Tests/TheTests.ElementUsage.01.verified.png):

<img src="/src/Tests/TheTests.ElementUsage.01.verified.png">


## OS specific rendering

The rendering can very slightly between different OS versions. This can make verification on different machines (eg CI) problematic. A [custom comparer](https://github.com/VerifyTests/Verify/blob/master/docs/comparer.md) can to mitigate this.


## Security contact information

To report a security vulnerability, use the [Tidelift security contact](https://tidelift.com/security). Tidelift will coordinate the fix and disclosure.


## Icon

[Gem](https://thenounproject.com/term/gem/2247823/) designed by [Adnen Kadri](https://thenounproject.com/adnen.kadri/) from [The Noun Project](https://thenounproject.com/creativepriyanka).