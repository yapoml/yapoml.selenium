Generates page object classes for Selenium WebDriver with ease.

# Installation
Install [Yapoml.Selenium](https://www.nuget.org/packages/Yapoml.Selenium) nuget package and create your `*.page.yaml` files.

# Usage
Given that you have the following `Login.page.yaml` file

```yaml
username input: id username
```

Then you are immediately able to interact with web elements

```csharp
webDriver.Ya().LoginPage.UsernameInput.SendKeys("user01");
```
