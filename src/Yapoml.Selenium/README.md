Generates page object classes for Selenium WebDriver with ease.

Given that you have the following `LoginPage.po.yaml` file

```yaml
ya:
  UsernameInput:
    by: id username

  PasswordInput:
    by:
      css: .password

  LoginButton:
    by: css .primary-button
```

Then you are able to immediately interact with web elements

```csharp
using Yapoml.Selenium;

webDriver.Ya().LoginPage.UsernameInput.SendKeys("user01");
```