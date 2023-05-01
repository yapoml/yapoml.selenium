using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Yapoml.Selenium.Sample.SwagLabs.Pages;

namespace Yapoml.Selenium.Sample.SwagLabs
{
    internal class SwagLabsTests
    {
        private IWebDriver _webDriver;
        private PagesSpace _ya;

        [SetUp]
        public void SetUp()
        {
            _webDriver = new ChromeDriver();
            _ya = _webDriver.Ya(opts => opts.WithBaseUrl("https://www.saucedemo.com/")).SwagLabs.Pages;
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver?.Quit();
        }

        [Test]
        public void Login()
        {
            _ya.LoginPage.Open().Form
                .Username.Type("standard_user")
                .And.Password.Type("secret_sauce")
                .And.Login.Click();
        }

        [Test]
        public void IncorrectLogin()
        {
            var error = _ya.LoginPage.Open().Form
                .Login.Click().And.Error;

            Assert.That(error.Displayed, Is.True);
            Assert.That(error.Text, Is.EqualTo("Epic sadface: Username is required"));

            error.Close.Click();

            Assert.That(error.Displayed, Is.False);
        }
    }
}
