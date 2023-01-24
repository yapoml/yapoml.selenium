Generates page object classes for Selenium WebDriver with ease.

Given that you have the following `Login.page.yaml` file

```yaml
username: id username

login: .//button
```

Then you are immediately able to interact with web elements

```csharp
webDriver.Ya().LoginPage.Username.SendKeys("user01");
```