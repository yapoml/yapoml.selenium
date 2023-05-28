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

        public string A
            => "qwe";

        [Test]
        public void Login()
        {
            _ya.LoginPage.Open().Form
                .Username.Type("standard_user")
                .Password.Type("secret_sauce")
                .Login.Click();
        }

        [Test]
        public void IncorrectLogin()
        {
            var error = _ya.LoginPage.Open().Form
                .Login.Click().Error;

            Assert.That(error.IsDisplayed, Is.True);
            Assert.That(error.Text, Is.EqualTo("Epic sadface: Username is required"));

            error.Close.Click();

            Assert.That(error.IsDisplayed, Is.False);
        }
    }
}
