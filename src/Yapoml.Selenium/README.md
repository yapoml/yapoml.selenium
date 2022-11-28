Generates page object classes for Selenium WebDriver with ease.

Given that you have the following `Login.page.yaml` file

```yaml
UsernameInput: id username

PasswordInput: .password

LoginButton: .//button
```

Then you are able to immediately interact with web elements

```csharp
webDriver.Ya().Login.UsernameInput.SendKeys("user01");
```