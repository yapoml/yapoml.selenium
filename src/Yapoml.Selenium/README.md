Generates page object classes for Selenium WebDriver with ease.

Given that you have the following `Login.page.yaml` file

```yaml
username: id username

login: .//button
```

Then you are able to immediately interact with web elements

```csharp
driver.Ya().LoginPage.Username.Type("user01");
```